using Android.Provider;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinMusicPlayer.Droid.Extensions;
using XamarinMusicPlayer.Models;
using XamarinMusicPlayer.ServiceInterfaces;

[assembly: Dependency(typeof(XamarinMusicPlayer.Droid.Services.DroidArtistService))]
namespace XamarinMusicPlayer.Droid.Services
{
    public class DroidArtistService : IArtistService
    {
        private static readonly string[] _projections = new[]
        {
            MediaStore.Audio.Artists.InterfaceConsts.Id,
            MediaStore.Audio.Artists.InterfaceConsts.Artist,
            MediaStore.Audio.Artists.InterfaceConsts.NumberOfTracks
        };

        public async Task<IEnumerable<Artist>> GetArtistsAsync(ArtistSortOrder sortOrder = ArtistSortOrder.Default)
        {
            return await LoadArtistsAsync(
                selection: null,
                selectionArgs: null,
                sortOrder: sortOrder
                 );
        }

        public async Task<IEnumerable<Artist>> GetArtistsByGenre(string genreId, ArtistSortOrder sortOrder = ArtistSortOrder.Default)
        {
            return await LoadArtistsAsync(
                selection: null,
                selectionArgs: null,
                sortOrder: sortOrder
                 );
        }

        public async Task<IEnumerable<Artist>> GetArtistsByIdAsync(IEnumerable<string> artistIds, ArtistSortOrder sortOrder = ArtistSortOrder.Default)
        {
            if (!artistIds.HasValue())
                throw new ArgumentException(nameof(artistIds));

            var selectionBuilder = new StringBuilder();
            selectionBuilder.Append(MediaStore.Audio.Artists.InterfaceConsts.Id + " IN (");
            selectionBuilder.Append(string.Join(",", artistIds));
            selectionBuilder.Append(")");

            return await LoadArtistsAsync(
                    selection: selectionBuilder.ToString(),
                    selectionArgs: null,
                    sortOrder: sortOrder
                    );
        }

        public async Task<IEnumerable<Artist>> SearchArtistsAsync(string searchTerm, ArtistSortOrder sortOrder = ArtistSortOrder.Default)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                throw new ArgumentNullException(nameof(searchTerm));

            return await LoadArtistsAsync(
                    selection: MediaStore.Audio.ArtistColumns.Artist + " like ?",
                    selectionArgs: new[] { searchTerm + "%" },
                    sortOrder: sortOrder
                    );
        }

        private async Task<IEnumerable<Artist>> LoadArtistsAsync(string selection, string[] selectionArgs, ArtistSortOrder sortOrder)
        {
            return await Task.Run(() =>
            {
                var cursor = Android.App.Application.Context.ContentResolver.Query(
                               uri: MediaStore.Audio.Artists.ExternalContentUri,
                               projection: _projections,
                               selection: selection,
                               selectionArgs: selectionArgs,
                               sortOrder: sortOrder.ToStringSortOrder()
                            );
                return cursor.ToArtists();
            });
        }
    }
}