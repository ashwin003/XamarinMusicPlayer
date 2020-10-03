using Foundation;
using MediaPlayer;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinMusicPlayer.iOS.Extensions;
using XamarinMusicPlayer.Models;
using XamarinMusicPlayer.ServiceInterfaces;

[assembly: Dependency(typeof(XamarinMusicPlayer.iOS.Services.CupertinoGenreService))]
namespace XamarinMusicPlayer.iOS.Services
{
    public class CupertinoGenreService : IGenreService
    {
        public async Task<IEnumerable<Genre>> GetGenresAsync(GenreSortOrder sortOrder = GenreSortOrder.Default)
        {
            return await Task.Run(() =>
            {
                var mediaQuery = PrepareMediaQuery();

                return mediaQuery.ToGenres(sortOrder);
            });
        }

        public async Task<IEnumerable<Genre>> SearchGenresAsync(string searchTerm, GenreSortOrder sortOrder = GenreSortOrder.Default)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                throw new ArgumentNullException(nameof(searchTerm));

            return await Task.Run(() =>
            {
                var mediaQuery = PrepareMediaQuery();
                var value = NSObject.FromObject(searchTerm);
                var searchTermPredicate = MPMediaPropertyPredicate.PredicateWithValue(value, MPMediaItem.TitleProperty, MPMediaPredicateComparison.Contains);
                mediaQuery.AddFilterPredicate(searchTermPredicate);

                return mediaQuery.ToGenres(sortOrder);
            });
        }

        private MPMediaQuery PrepareMediaQuery()
        {
            var mediaQuery = MPMediaQuery.GenresQuery;
            mediaQuery.GroupingType = MPMediaGrouping.Genre;
            return mediaQuery;
        }
    }
}