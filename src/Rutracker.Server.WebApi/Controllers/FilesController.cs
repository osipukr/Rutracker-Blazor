﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.WebApi.Controllers.Base;
using Rutracker.Server.WebApi.Extensions;
using Rutracker.Shared.Models;
using Rutracker.Shared.Models.ViewModels.File;

namespace Rutracker.Server.WebApi.Controllers
{
    [Authorize(Policy = Policies.IsUser)]
    public class FilesController : BaseApiController
    {
        private readonly IFileService _fileService;

        public FilesController(IFileService fileService, IMapper mapper) : base(mapper)
        {
            _fileService = fileService;
        }

        [HttpGet("search"), AllowAnonymous]
        public async Task<IEnumerable<FileViewModel>> Search(int torrentId)
        {
            var files = await _fileService.ListAsync(torrentId);

            return _mapper.Map<IEnumerable<FileViewModel>>(files);
        }

        [HttpGet("{id}"), AllowAnonymous]
        public async Task<FileViewModel> Find(int id)
        {
            var file = await _fileService.FindAsync(id, User.GetUserId());

            return _mapper.Map<FileViewModel>(file);
        }

        [HttpPost]
        public async Task<FileViewModel> Add([FromForm] FileCreateViewModel model)
        {
            var stream = model.File.OpenReadStream();
            var result = await _fileService.AddAsync(User.GetUserId(), model.TorrentId, model.File.ContentType, model.File.FileName, stream);

            return _mapper.Map<FileViewModel>(result);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _fileService.DeleteAsync(id, User.GetUserId());
        }
    }
}