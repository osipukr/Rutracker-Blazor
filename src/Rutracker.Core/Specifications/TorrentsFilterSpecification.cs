﻿using System.Linq;
using Rutracker.Core.Entities;

namespace Rutracker.Core.Specifications
{
    public class TorrentsFilterSpecification : BaseSpecification<Torrent, long>
    {
        public TorrentsFilterSpecification(string search, string[] titles, long? sizeFrom, long? sizeTo)
            : base(x => (string.IsNullOrWhiteSpace(search) || x.Title.Contains(search)) 
                        && (titles == null || !titles.Any() || titles.Contains(x.Forum.Title))
                        && (!sizeFrom.HasValue || x.Size >= sizeFrom)
                        && (!sizeTo.HasValue || x.Size <= sizeTo))
        {
        }
    }
}