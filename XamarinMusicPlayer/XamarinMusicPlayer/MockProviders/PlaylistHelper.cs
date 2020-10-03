using System;
using System.Collections.Generic;
using System.Linq;
using XamarinMusicPlayer.Models;

namespace XamarinMusicPlayer.MockProviders
{
    public static class PlaylistHelper
    {
        public static IEnumerable<Playlist> Playlists = new[]
        {
            new Playlist
            {
                 Name = "Favorites",
                 CreationDate = DateTime.Now,
                 Id = 2,
                 MemberIds = new []{ "1", "2" }.ToList()
            },
            new Playlist
            {
                 Name = "Most Played",
                 CreationDate = DateTime.Now,
                 Id = 2,
                 MemberIds = new []{ "1", "2" }.ToList()
            }
        };
    }
}
