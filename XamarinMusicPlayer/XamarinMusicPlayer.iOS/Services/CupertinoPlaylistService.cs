using Foundation;
using MediaPlayer;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinMusicPlayer.iOS.Extensions;
using XamarinMusicPlayer.Models;
using XamarinMusicPlayer.ServiceInterfaces;

[assembly: Dependency(typeof(XamarinMusicPlayer.iOS.Services.CupertinoPlaylistService))]
namespace XamarinMusicPlayer.iOS.Services
{
    public class CupertinoPlaylistService : IPlaylistService
    {
        public async Task<IEnumerable<Playlist>> GetPlaylistsAsync(PlaylistSortOrder sortOrder = PlaylistSortOrder.Default)
        {
            return await Task.Run(() =>
            {
                var mediaQuery = PrepareMediQuery();

                return mediaQuery.ToPlaylists(sortOrder);
            });
        }

        public async Task<IEnumerable<Playlist>> SearchPlaylistsAsync(string searchTerm, PlaylistSortOrder sortOrder = PlaylistSortOrder.Default)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                throw new ArgumentNullException(nameof(searchTerm));

            return await Task.Run(() =>
            {
                var mediaQuery = PrepareMediQuery();
                var value = NSObject.FromObject(searchTerm);
                var searchTermPredicate = MPMediaPropertyPredicate.PredicateWithValue(value, MPMediaItem.TitleProperty, MPMediaPredicateComparison.Contains);
                mediaQuery.AddFilterPredicate(searchTermPredicate);

                return mediaQuery.ToPlaylists(sortOrder);
            });
        }

        private MPMediaQuery PrepareMediQuery()
        {
            var mediaQuery = MPMediaQuery.PlaylistsQuery;
            mediaQuery.GroupingType = MPMediaGrouping.Playlist;
            return mediaQuery;
        }
    }
}