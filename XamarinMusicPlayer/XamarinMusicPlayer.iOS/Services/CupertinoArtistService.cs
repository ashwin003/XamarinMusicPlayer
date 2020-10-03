using Foundation;
using MediaPlayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinMusicPlayer.iOS.Extensions;
using XamarinMusicPlayer.Models;
using XamarinMusicPlayer.ServiceInterfaces;

[assembly: Dependency(typeof(XamarinMusicPlayer.iOS.Services.CupertinoArtistService))]
namespace XamarinMusicPlayer.iOS.Services
{
    public class CupertinoArtistService : IArtistService
    {
        public async Task<IEnumerable<Artist>> GetArtistsAsync(ArtistSortOrder sortOrder = ArtistSortOrder.Default)
        {
            return await Task.Run(() =>
            {
                var mediaQuery = PrepareMediaQuery();

                return mediaQuery.ToArtists(sortOrder);
            });
        }

        public async Task<IEnumerable<Artist>> GetArtistsByGenre(string genreId, ArtistSortOrder sortOrder = ArtistSortOrder.Default)
        {
            if (string.IsNullOrWhiteSpace(genreId))
                throw new ArgumentNullException(nameof(genreId));

            return await Task.Run(() =>
            {
                var mediaQuery = PrepareMediaQuery();
                var value = NSObject.FromObject(genreId);
                var genreIdPredicate = MPMediaPropertyPredicate.PredicateWithValue(value, MPMediaItem.GenrePersistentIDProperty);
                mediaQuery.AddFilterPredicate(genreIdPredicate);

                return mediaQuery.ToArtists(sortOrder);
            });
        }

        public async Task<IEnumerable<Artist>> GetArtistsByIdAsync(IEnumerable<string> artistIds, ArtistSortOrder sortOrder = ArtistSortOrder.Default)
        {
            if (!artistIds.HasValue())
                throw new ArgumentException(nameof(artistIds));

            return await Task.Run(() =>
            {
                var mediaQuery = PrepareMediaQuery();
                var value = NSArray.FromNSObjects(artistIds.Select(s => NSObject.FromObject(s)).ToArray());
                var arrayPredictor = MPMediaPropertyPredicate.PredicateWithValue(value, MPMediaItem.PersistentIDProperty, MPMediaPredicateComparison.Contains);
                mediaQuery.AddFilterPredicate(arrayPredictor);

                return mediaQuery.ToArtists(sortOrder);
            });
        }

        public async Task<IEnumerable<Artist>> SearchArtistsAsync(string searchTerm, ArtistSortOrder sortOrder = ArtistSortOrder.Default)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                throw new ArgumentNullException(nameof(searchTerm));

            return await Task.Run(() =>
            {
                var mediaQuery = PrepareMediaQuery();
                var value = NSObject.FromObject(searchTerm);
                var searchTermPredicate = MPMediaPropertyPredicate.PredicateWithValue(value, MPMediaItem.TitleProperty, MPMediaPredicateComparison.Contains);
                mediaQuery.AddFilterPredicate(searchTermPredicate);

                return mediaQuery.ToArtists(sortOrder);
            });
        }

        private MPMediaQuery PrepareMediaQuery()
        {
            var mediaQuery = MPMediaQuery.ArtistsQuery;
            mediaQuery.GroupingType = MPMediaGrouping.Artist;
            return mediaQuery;
        }
    }
}