﻿using System.Threading.Tasks;
using Rutracker.Client.Blazor.Interfaces;
using Rutracker.Client.Blazor.Settings;
using Rutracker.Shared.Models.ViewModels.Shared;
using Rutracker.Shared.Models.ViewModels.Torrent;

namespace Rutracker.Client.Blazor.Services
{
    public class TorrentService : ITorrentService
    {
        private readonly HttpClientService _httpClientService;
        private readonly ApiUriSettings _apiUriSettings;

        public TorrentService(HttpClientService httpClient, ApiUriSettings uriSettings)
        {
            _httpClientService = httpClient;
            _apiUriSettings = uriSettings;
        }

        public async Task<PaginationResult<TorrentViewModel>> Torrents(int page, int pageSize, FilterViewModel filter)
        {
            var url = string.Format(_apiUriSettings.TorrentsIndex, page.ToString(), pageSize.ToString());

            return await _httpClientService.PostJsonAsync<PaginationResult<TorrentViewModel>>(url, filter);
        }

        public async Task<TorrentDetailsViewModel> Torrent(long id)
        {
            var url = string.Format(_apiUriSettings.TorrentIndex, id.ToString());

            return await _httpClientService.GetJsonAsync<TorrentDetailsViewModel>(url);
        }

        public async Task<FacetResult<string>> TitleFacet(int count)
        {
            var url = string.Format(_apiUriSettings.Titles, count.ToString());

            return await _httpClientService.GetJsonAsync<FacetResult<string>>(url);
        }
    }
}