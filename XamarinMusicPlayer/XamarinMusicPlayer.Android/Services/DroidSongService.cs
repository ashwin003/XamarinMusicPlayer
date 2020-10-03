using Android.Provider;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinMusicPlayer.Droid.Extensions;
using XamarinMusicPlayer.Models;
using XamarinMusicPlayer.ServiceInterfaces;

[assembly: Dependency(typeof(XamarinMusicPlayer.Droid.Services.DroidSongService))]
namespace XamarinMusicPlayer.Droid.Services
{
    public class DroidSongService : ISongService
    {
        private static readonly string[] _projections = new[]
        {
            MediaStore.Audio.Media.InterfaceConsts.Id,

            MediaStore.Audio.Media.InterfaceConsts.ArtistId,

            MediaStore.Audio.Media.InterfaceConsts.AlbumId,

            MediaStore.Audio.Media.InterfaceConsts.Composer,

            MediaStore.Audio.Media.InterfaceConsts.Title,
            MediaStore.Audio.Media.InterfaceConsts.DisplayName,

            MediaStore.Audio.Media.InterfaceConsts.Duration,

            MediaStore.Audio.Media.InterfaceConsts.Track,
            MediaStore.Audio.Media.InterfaceConsts.Data,
            MediaStore.Audio.Media.InterfaceConsts.Size,

            MediaStore.Audio.Media.InterfaceConsts.Year,

            MediaStore.Audio.Media.InterfaceConsts.IsMusic,
            MediaStore.Audio.Media.InterfaceConsts.IsAlarm,
            MediaStore.Audio.Media.InterfaceConsts.IsNotification,
            MediaStore.Audio.Media.InterfaceConsts.IsPodcast,
        };

        public async Task<IEnumerable<Song>> GetSongsAsync(SongSortOrder sortOrder = SongSortOrder.Default)
        {
            return await LoadSongsAsync(
                    selection: null,
                    selectionArgs: null,
                    sortOrder: sortOrder
                    );
        }

        public async Task<IEnumerable<Song>> GetSongsByIdAsync(IEnumerable<string> songIds, SongSortOrder sortOrder = SongSortOrder.Default)
        {
            if (!songIds.HasValue())
                throw new ArgumentException(nameof(songIds));

            var selectionBuilder = new StringBuilder();
            selectionBuilder.Append(MediaStore.Audio.Media.InterfaceConsts.Id + " IN (");
            selectionBuilder.Append(string.Join(",", songIds));
            selectionBuilder.Append(")");

            return await LoadSongsAsync(
                    selection: selectionBuilder.ToString(),
                    selectionArgs: null,
                    sortOrder: sortOrder
                    );
        }

        public async Task<IEnumerable<Song>> GetSongsFromAlbumAsync(string albumId, SongSortOrder sortOrder = SongSortOrder.Default)
        {
            if (string.IsNullOrWhiteSpace(albumId))
                throw new ArgumentNullException(nameof(albumId));

            return await LoadSongsAsync(
                    selection: MediaStore.Audio.Media.InterfaceConsts.AlbumId + " =?",
                    selectionArgs: new[] { albumId },
                    sortOrder: sortOrder
                    );
        }

        public async Task<IEnumerable<Song>> GetSongsFromArtistAsync(string artistId, SongSortOrder sortOrder = SongSortOrder.Default)
        {
            if (string.IsNullOrWhiteSpace(artistId))
                throw new ArgumentNullException(nameof(artistId));

            return await LoadSongsAsync(
                    selection: MediaStore.Audio.Media.InterfaceConsts.ArtistId + " =?",
                    selectionArgs: new[] { artistId },
                    sortOrder: sortOrder
                    );
        }

        public async Task<IEnumerable<Song>> GetSongsFromGenreAsync(string genreId, SongSortOrder sortOrder = SongSortOrder.Default)
        {
            if (string.IsNullOrWhiteSpace(genreId))
                throw new ArgumentNullException(nameof(genreId));

            return await LoadSongsAsync(
                    selection: null,
                    selectionArgs: null,
                    sortOrder: sortOrder
                    );
        }

        public async Task<IEnumerable<Song>> GetSongsFromPlaylistAsync(Playlist playlist, SongSortOrder sortOrder = SongSortOrder.Default) 
        {
            if (playlist == null)
                throw new ArgumentNullException(nameof(playlist));

            return await GetSongsByIdAsync(playlist.MemberIds, sortOrder);
        }

        public async Task<IEnumerable<Song>> SearchSongsAsync(string searchTerm, SongSortOrder sortOrder = SongSortOrder.Default)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                throw new ArgumentNullException(nameof(searchTerm));

            return await LoadSongsAsync(
                    selection: MediaStore.Audio.Media.InterfaceConsts.Title + " like ?",
                    selectionArgs: new[] { searchTerm + "%" },
                    sortOrder: sortOrder
                    );
        }

        private async Task<IEnumerable<Song>> LoadSongsAsync(string selection, string[] selectionArgs, SongSortOrder sortOrder)
        {
            return await Task.Run(() =>
            {
                var cursor = Android.App.Application.Context.ContentResolver.Query(
                                uri: MediaStore.Audio.Media.ExternalContentUri,
                                projection: _projections,
                                selection: selection,
                                selectionArgs: selectionArgs,
                                sortOrder: sortOrder.ToStringSortOrder()
                             );
                return cursor.ToSongs();
            });
        }
    }
}