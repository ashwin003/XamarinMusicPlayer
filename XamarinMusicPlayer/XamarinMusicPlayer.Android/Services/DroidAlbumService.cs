using Android.Provider;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinMusicPlayer.Droid.Extensions;
using XamarinMusicPlayer.Models;
using XamarinMusicPlayer.ServiceInterfaces;

[assembly: Dependency(typeof(XamarinMusicPlayer.Droid.Services.DroidAlbumService))]
namespace XamarinMusicPlayer.Droid.Services
{
    public class DroidAlbumService : IAlbumService
    {
        private static readonly string[] _projections = new[] {
            MediaStore.Audio.Albums.InterfaceConsts.Id,
            MediaStore.Audio.Albums.InterfaceConsts.Album,
            MediaStore.Audio.Albums.InterfaceConsts.Artist,
            MediaStore.Audio.Albums.InterfaceConsts.FirstYear,
            MediaStore.Audio.Albums.InterfaceConsts.LastYear,
            MediaStore.Audio.Albums.InterfaceConsts.NumberOfSongs
        };

        public async Task<IEnumerable<Album>> GetAlbumsAsync(AlbumSortOrder sortOrder = AlbumSortOrder.Default)
        {
            return await LoadAlbumsAsync(
                selection: null,
                selectionArgs: null,
                sortOrder: sortOrder
                );
        }

        public async Task<IEnumerable<Album>> GetAlbumsByGenreAsync(string genreId, AlbumSortOrder sortOrder = AlbumSortOrder.Default)
        {
            return await LoadAlbumsAsync(
                selection: null,
                selectionArgs: null,
                sortOrder: sortOrder
                );
        }

        public async Task<IEnumerable<Album>> GetAlbumsByIdAsync(IEnumerable<string> albumIds, AlbumSortOrder sortOrder = AlbumSortOrder.Default)
        {
            if(!albumIds.HasValue())
                throw new ArgumentException(nameof(albumIds));

            var selectionBuilder = new StringBuilder();
            selectionBuilder.Append(MediaStore.Audio.AlbumColumns.AlbumId + " IN (");
            selectionBuilder.Append(string.Join(",", albumIds));
            selectionBuilder.Append(")");

            return await LoadAlbumsAsync(
                    selection: selectionBuilder.ToString(),
                    selectionArgs: null,
                    sortOrder: sortOrder
                    );
        }

        public async Task<IEnumerable<Album>> SearchAlbumsAsync(string searchTerm, AlbumSortOrder sortOrder = AlbumSortOrder.Default)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                throw new ArgumentNullException(nameof(searchTerm));

            return await LoadAlbumsAsync(
                    selection: MediaStore.Audio.AlbumColumns.Album + " like ?",
                    selectionArgs: new[] { searchTerm + "%" },
                    sortOrder: sortOrder
                    );
        }

        private async Task<IEnumerable<Album>> LoadAlbumsAsync(string selection, string[] selectionArgs, AlbumSortOrder sortOrder)
        {
            return await Task.Run(() =>
            {
                var cursor = Android.App.Application.Context.ContentResolver.Query(
                                uri: MediaStore.Audio.Albums.ExternalContentUri,
                                projection: _projections,
                                selection: selection,
                                selectionArgs: selectionArgs,
                                sortOrder: sortOrder.ToStringSortOrder()
                             );

                return cursor.ToAlbums();
            });
        }
    }
}