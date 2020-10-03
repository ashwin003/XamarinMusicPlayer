using Android.Provider;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinMusicPlayer.Droid.Extensions;
using XamarinMusicPlayer.Models;
using XamarinMusicPlayer.ServiceInterfaces;

[assembly: Dependency(typeof(XamarinMusicPlayer.Droid.Services.DroidPlaylistService))]
namespace XamarinMusicPlayer.Droid.Services
{
    public class DroidPlaylistService : IPlaylistService
    {
        private static string[] _projections = new[]
        {
            MediaStore.Audio.Playlists.InterfaceConsts.Id,
            MediaStore.Audio.Playlists.InterfaceConsts.Name,
            MediaStore.Audio.Playlists.InterfaceConsts.DateAdded
        };

        public async Task<IEnumerable<Playlist>> GetPlaylistsAsync(PlaylistSortOrder sortOrder = PlaylistSortOrder.Default)
        {
            return await LoadPlaylistsAsync(
                selection: null,
                selectionArgs: null,
                sortOrder: sortOrder
                );
        }

        public async Task<IEnumerable<Playlist>> SearchPlaylistsAsync(string searchTerm, PlaylistSortOrder sortOrder = PlaylistSortOrder.Default)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                throw new ArgumentNullException(nameof(searchTerm));

            return await LoadPlaylistsAsync(
                    selection: MediaStore.Audio.PlaylistsColumns.Name + " like ?",
                    selectionArgs: new[] { searchTerm + "%" },
                    sortOrder: sortOrder
                    );
        }

        private async Task<IEnumerable<Playlist>> LoadPlaylistsAsync(string selection, string[] selectionArgs, PlaylistSortOrder sortOrder)
        {
            return await Task.Run(() =>
            {
                var cursor = Android.App.Application.Context.ContentResolver.Query(
                                uri: MediaStore.Audio.Playlists.ExternalContentUri,
                                projection: _projections,
                                selection: selection,
                                selectionArgs: selectionArgs,
                                sortOrder: sortOrder.ToStringSortOrder()
                             );
                return cursor.ToPlaylists();
            });
        }
    }
}