﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Microsoft.Extensions.Options;
using Rutracker.Core.Entities.Identity;
using Rutracker.Server.Interfaces;
using Rutracker.Server.Settings;
using Rutracker.Shared.ViewModels.Accounts;

namespace Rutracker.Server.Services
{
    public class JwtFactory : IJwtFactory
    {
        private readonly JwtSettings _jwtOptions;

        public JwtFactory(IOptions<JwtSettings> jwtOptions)
        {
            Guard.Against.Null(jwtOptions, nameof(jwtOptions));

            _jwtOptions = jwtOptions.Value;

            Guard.Against.Null(_jwtOptions, nameof(_jwtOptions));

            if (_jwtOptions.ValidFor <= TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(_jwtOptions.ValidFor));
            }

            Guard.Against.Null(_jwtOptions.SigningCredentials, nameof(_jwtOptions.SigningCredentials));
            Guard.Against.Null(_jwtOptions.JtiGenerator, nameof(_jwtOptions.JtiGenerator));
        }

        public async Task<JwtToken> GenerateTokenAsync(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64)
            };

            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: _jwtOptions.NotBefore,
                expires: _jwtOptions.Expiration,
                signingCredentials: _jwtOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JwtToken
            {
                Token = encodedJwt,
                ExpiresIn = (long)_jwtOptions.ValidFor.TotalSeconds
            };
        }

        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        private static long ToUnixEpochDate(DateTime date) =>
            (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}