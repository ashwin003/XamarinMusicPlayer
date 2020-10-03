using Android.Provider;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinMusicPlayer.Droid.Extensions;
using XamarinMusicPlayer.Models;
using XamarinMusicPlayer.ServiceInterfaces;

[assembly: Dependency(typeof(XamarinMusicPlayer.Droid.Services.DroidGenreService))]
namespace XamarinMusicPlayer.Droid.Services
{
    public class DroidGenreService : IGenreService
    {
        private static readonly string[] _projections = new[]
        {
            MediaStore.Audio.Genres.InterfaceConsts.Id,
            MediaStore.Audio.Genres.InterfaceConsts.Name
        };

        public async Task<IEnumerable<Genre>> GetGenresAsync(GenreSortOrder sortOrder = GenreSortOrder.Default)
        {
            return await LoadGenresAsync(
                selection: null,
                selectionArgs: null,
                sortOrder: sortOrder
                );
        }

        public async Task<IEnumerable<Genre>> SearchGenresAsync(string searchTerm, GenreSortOrder sortOrder = GenreSortOrder.Default)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                throw new ArgumentNullException(nameof(searchTerm));

            return await LoadGenresAsync(
                    selection: MediaStore.Audio.GenresColumns.Name + " like ?",
                    selectionArgs: new[] { searchTerm + "%" },
                    sortOrder: sortOrder
                    );
        }

        private async Task<IEnumerable<Genre>> LoadGenresAsync(string selection, string[] selectionArgs, GenreSortOrder sortOrder)
        {
            return await Task.Run(() =>
            {
                using var cursor = Android.App.Application.Context.ContentResolver.Query(
                                uri: MediaStore.Audio.Genres.ExternalContentUri,
                                projection: _projections,
                                selection: selection,
                                selectionArgs: selectionArgs,
                                sortOrder: sortOrder.ToStringSortOrder()
                             );
                return cursor.ToGenres();
            });
        }
    }
}