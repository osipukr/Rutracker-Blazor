﻿using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Rutracker.Server.Interfaces;

namespace Rutracker.Server.Controllers
{
    public class TorrentController : BaseController
    {
        private readonly ITorrentService _torrentService;

        public TorrentController(ITorrentService torrentService)
        {
            _torrentService = torrentService;
        }

        [Route("torrents")]
        [HttpGet("{page}/{pageSize}/{search?}")]
        public async Task<IActionResult> GetTorrentsAsync(int page, int pageSize, string search)
        {
            try
            {
                var result = await _torrentService.GetTorrentsIndexAsync(page, pageSize, search);

                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Route("details")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTorrentAsync(long id)
        {
            try
            {
                var result = await _torrentService.GetTorrentIndexAsync(id);

                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Route("titles")]
        [HttpGet("{count}")]
        public async Task<IActionResult> GetTitlesAsync(int count)
        {
            try
            {
                var result = await _torrentService.GetTitlesAsync(count);

                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}