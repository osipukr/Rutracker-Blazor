﻿using AutoMapper;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Shared.Models.ViewModels.Torrent;

namespace Rutracker.Server.WebApi.Mapping
{
    public class ForumViewModelProfile : Profile
    {
        public ForumViewModelProfile() => CreateMap<Forum, ForumViewModel>();
    }
}