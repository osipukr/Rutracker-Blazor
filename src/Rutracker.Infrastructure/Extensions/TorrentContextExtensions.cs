﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rutracker.Core.Entities;
using Rutracker.Infrastructure.Data.Contexts;

namespace Rutracker.Infrastructure.Extensions
{
    public static class TorrentContextExtensions
    {
        public static async Task SeedAsync(this TorrentContext context)
        {
            try
            {
                if (!await context.Forums.AnyAsync())
                {
                    var files = GetPreconfiguredForums();

                    await context.Forums.AddRangeAsync(files);
                    await context.SaveChangesAsync();
                }

                if (!await context.Torrents.AnyAsync())
                {
                    var torrents = GetPreconfiguredTorrents();

                    await context.Torrents.AddRangeAsync(torrents);
                    await context.SaveChangesAsync();
                }

                if (!await context.Files.AnyAsync())
                {
                    var files = GetPreconfiguredFiles();

                    await context.Files.AddRangeAsync(files);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                // logger
                Console.WriteLine(ex.Message);
            }
        }

        #region GetPreconfiguredItems

        private static readonly Random Random = new Random();
        private const int ForumMaxCount = 30;
        private const int TorrentMaxCount = 200;
        private const int FileMaxCount = 500;

        private static IEnumerable<Forum> GetPreconfiguredForums()
        {
            for (var i = 1; i <= ForumMaxCount; i++)
            {
                yield return new Forum
                {
                    Id = i,
                    Title = Guid.NewGuid().ToString()
                };
            }
        }

        private static IEnumerable<Torrent> GetPreconfiguredTorrents()
        {
            for (var i = 1; i <= TorrentMaxCount; i++)
            {
                yield return new Torrent
                {
                    Id = i,
                    Date = DateTime.Now,
                    Size = Random.Next(1, int.MaxValue),
                    Title = Guid.NewGuid().ToString(),
                    Hash = Guid.NewGuid().ToString(),
                    Content = Guid.NewGuid().ToString(),
                    TrackerId = Random.Next(1, int.MaxValue),
                    ForumId = Random.Next(1, ForumMaxCount)
                };
            }
        }

        private static IEnumerable<File> GetPreconfiguredFiles()
        {
            for (var i = 1; i <= FileMaxCount; i++)
            {
                yield return new File
                {
                    Name = Guid.NewGuid().ToString(),
                    Size = Random.Next(1, int.MaxValue),
                    TorrentId = Random.Next(1, TorrentMaxCount)
                };
            }
        }

        #endregion
    }
}