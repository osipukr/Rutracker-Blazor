﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blazor.FileReader;
using Rutracker.Client.BlazorWasm.Helpers;
using Rutracker.Client.BlazorWasm.Interfaces;
using Rutracker.Client.BlazorWasm.Settings;
using Rutracker.Shared.Models;
using Rutracker.Shared.Models.ViewModels.Torrent;

namespace Rutracker.Client.BlazorWasm.Services
{
    public class TorrentService : ITorrentService
    {
        private readonly HttpClientService _httpClientService;
        private readonly ApiUrlSettings _apiUrls;

        public TorrentService(HttpClientService httpClientService, ApiUrlSettings apiUrls)
        {
            _httpClientService = httpClientService;
            _apiUrls = apiUrls;
        }

        public async Task<PaginationResult<TorrentViewModel>> ListAsync(int page, int pageSize, TorrentFilterViewModel filter)
        {
            var url = string.Format(_apiUrls.TorrentsSearch, page.ToString(), pageSize.ToString());

            return await _httpClientService.PostJsonAsync<PaginationResult<TorrentViewModel>>(url, filter);
        }

        public async Task<IEnumerable<TorrentViewModel>> PopularAsync(int count)
        {
            var url = string.Format(_apiUrls.PopularTorrentsSearch, count.ToString());

            return await _httpClientService.GetJsonAsync<IEnumerable<TorrentViewModel>>(url);
        }

        public async Task<TorrentDetailsViewModel> FindAsync(int id)
        {
            var url = string.Format(_apiUrls.Torrent, id.ToString());

            return await _httpClientService.GetJsonAsync<TorrentDetailsViewModel>(url);
        }

        public async Task<TorrentDetailsViewModel> CreateAsync(TorrentCreateViewModel model)
        {
            return await _httpClientService.PostJsonAsync<TorrentDetailsViewModel>(_apiUrls.Torrents, model);
        }

        public async Task<TorrentDetailsViewModel> UpdateAsync(int id, TorrentUpdateViewModel model)
        {
            var url = string.Format(_apiUrls.Torrent, id.ToString());

            return await _httpClientService.PutJsonAsync<TorrentDetailsViewModel>(url, model);
        }

        public async Task DeleteAsync(int id)
        {
            var url = string.Format(_apiUrls.Torrent, id.ToString());

            await _httpClientService.DeleteJsonAsync(url);
        }

        public async Task<string> ChangeImageAsync(int id, ChangeTorrentImageViewModel model)
        {
            var url = string.Format(_apiUrls.TorrentImage, id.ToString());

            return await _httpClientService.PutJsonAsync<string>(url, model);
        }

        public async Task<string> ChangeImageAsync(int id, IFileReference imageReference)
        {
            if (imageReference == null)
            {
                throw new Exception("File is not selected.");
            }

            var fileInfo = await imageReference.ReadFileInfoAsync();

            if (fileInfo.Size > Constants.File.MaxImageSize)
            {
                throw new Exception("File is too large");
            }

            var stream = await imageReference.OpenReadAsync();
            var url = string.Format(_apiUrls.TorrentImage, id.ToString());

            return await _httpClientService.PostTorrentImageFileAsync<string>(url, fileInfo.Type, fileInfo.Name, stream);
        }

        public async Task DeleteImageAsync(int id)
        {
            var url = string.Format(_apiUrls.TorrentImage, id.ToString());

            await _httpClientService.DeleteJsonAsync(url);
        }
    }
}