﻿namespace Rutracker.Shared.Models.ViewModels
{
    public class PaginationResult<TResult>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 0;
        public int TotalPages { get; set; } = 0;
        public TResult[] Items { get; set; }
    }
}