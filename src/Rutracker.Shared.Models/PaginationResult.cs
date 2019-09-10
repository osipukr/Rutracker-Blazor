﻿using System;
using System.Collections.Generic;

namespace Rutracker.Shared.Models
{
    public class PaginationResult<TResult>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalItems / (double)PageSize);
        public IEnumerable<TResult> Items { get; set; }
    }
}