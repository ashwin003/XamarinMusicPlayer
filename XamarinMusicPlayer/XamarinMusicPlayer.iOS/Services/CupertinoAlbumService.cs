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

[assembly: Dependency(typeof(XamarinMusicPlayer.iOS.Services.CupertinoAlbumService))]
namespace XamarinMusicPlayer.iOS.Services
{
    public class CupertinoAlbumService : IAlbumService
    {
        public async Task<IEnumerable<Album>> GetAlbumsAsync(AlbumSortOrder sortOrder = AlbumSortOrder.Default)
        {
            return await Task.Run(() =>
            {
                var mediaQuery = PrepareMediaQuery();

                return mediaQuery.ToAlbums(sortOrder);
            });
        }

        public async Task<IEnumerable<Album>> GetAlbumsByGenreAsync(string genreId, AlbumSortOrder sortOrder = AlbumSortOrder.Default)
        {
            if (string.IsNullOrWhiteSpace(genreId))
                throw new ArgumentNullException(nameof(genreId));

            return await Task.Run(() =>
            {
                var mediaQuery = PrepareMediaQuery();
                var value = NSObject.FromObject(genreId);
                var genreIdPredicate = MPMediaPropertyPredicate.PredicateWithValue(value, MPMediaItem.GenrePersistentIDProperty);
                mediaQuery.AddFilterPredicate(genreIdPredicate);

                return mediaQuery.ToAlbums(sortOrder);
            });
        }

        public async Task<IEnumerable<Album>> GetAlbumsByIdAsync(IEnumerable<string> albumIds, AlbumSortOrder sortOrder = AlbumSortOrder.Default)
        {
            if (!albumIds.HasValue())
                throw new ArgumentException(nameof(albumIds));
            return await Task.Run(() =>
            {
                var mediaQuery = PrepareMediaQuery();
                var value = NSArray.FromNSObjects(albumIds.Select(s => NSObject.FromObject(s)).ToArray());
                var arrayPredictor = MPMediaPropertyPredicate.PredicateWithValue(value, MPMediaItem.PersistentIDProperty, MPMediaPredicateComparison.Contains);
                mediaQuery.AddFilterPredicate(arrayPredictor);

                return mediaQuery.ToAlbums(sortOrder);
            });
        }

        public async Task<IEnumerable<Album>> SearchAlbumsAsync(string searchTerm, AlbumSortOrder sortOrder = AlbumSortOrder.Default)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                throw new ArgumentNullException(nameof(searchTerm));

            return await Task.Run(() =>
            {
                var mediaQuery = PrepareMediaQuery();
                var value = NSObject.FromObject(searchTerm);
                var searchTermPredicate = MPMediaPropertyPredicate.PredicateWithValue(value, MPMediaItem.TitleProperty, MPMediaPredicateComparison.Contains);
                mediaQuery.AddFilterPredicate(searchTermPredicate);

                return mediaQuery.ToAlbums(sortOrder);
            });
        }

        private MPMediaQuery PrepareMediaQuery()
        {
            var mediaQuery = MPMediaQuery.AlbumsQuery;
            mediaQuery.GroupingType = MPMediaGrouping.Album;
            return mediaQuery;
        }
    }
}