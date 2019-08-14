﻿namespace Rutracker.Client.Blazor.Settings
{
    public class ApiUriSettings
    {
        // Torrent
        public string TorrentsIndex { get; set; }
        public string TorrentIndex { get; set; }
        public string Titles { get; set; }

        // Account
        public string Login { get; set; }
        public string Register { get; set; }
        public string Logout { get; set; }

        // User
        public string Users { get; set; }
        public string UserDetails { get; set; }
        public string UpdateUser { get; set; }
    }
}