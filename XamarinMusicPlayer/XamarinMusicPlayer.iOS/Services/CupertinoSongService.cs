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

[assembly: Dependency(typeof(XamarinMusicPlayer.iOS.Services.CupertinoSongService))]
namespace XamarinMusicPlayer.iOS.Services
{
    public class CupertinoSongService : ISongService
    {
        public async Task<IEnumerable<Song>> GetSongsAsync(SongSortOrder sortOrder = SongSortOrder.Default)
        {
            return await Task.Run(() =>
            {
                var mediaQuery = PrepareMediaQuery();
                return mediaQuery.ToSongs(sortOrder);
            });
        }

        public async Task<IEnumerable<Song>> GetSongsByIdAsync(IEnumerable<string> songIds, SongSortOrder sortOrder = SongSortOrder.Default)
        {
            if (!songIds.HasValue())
                throw new ArgumentException(nameof(songIds));

            return await Task.Run(() =>
            {
                var mediaQuery = PrepareMediaQuery();
                var value = NSArray.FromNSObjects(songIds.Select(s => NSObject.FromObject(s)).ToArray());
                var arrayPredictor = MPMediaPropertyPredicate.PredicateWithValue(value, MPMediaItem.PersistentIDProperty, MPMediaPredicateComparison.Contains);
                mediaQuery.AddFilterPredicate(arrayPredictor);
                return mediaQuery.ToSongs(sortOrder);
            });
        }

        public async Task<IEnumerable<Song>> GetSongsFromAlbumAsync(string albumId, SongSortOrder sortOrder = SongSortOrder.Default)
        {
            if (string.IsNullOrWhiteSpace(albumId))
                throw new ArgumentNullException(nameof(albumId));

            return await Task.Run(() =>
            {
                var mediaQuery = PrepareMediaQuery();
                var value = NSObject.FromObject(albumId);
                var albumIdpredicate = MPMediaPropertyPredicate.PredicateWithValue(value, MPMediaItem.AlbumPersistentIDProperty);
                mediaQuery.AddFilterPredicate(albumIdpredicate);
                return mediaQuery.ToSongs(sortOrder);
            });
        }

        public async Task<IEnumerable<Song>> GetSongsFromArtistAsync(string artistId, SongSortOrder sortOrder = SongSortOrder.Default)
        {
            if (string.IsNullOrWhiteSpace(artistId))
                throw new ArgumentNullException(nameof(artistId));

            return await Task.Run(() =>
            {
                var mediaQuery = PrepareMediaQuery();
                var value = NSObject.FromObject(artistId);
                var artistIdPredicate = MPMediaPropertyPredicate.PredicateWithValue(value, MPMediaItem.AlbumArtistPersistentIDProperty);
                mediaQuery.AddFilterPredicate(artistIdPredicate);
                return mediaQuery.ToSongs(sortOrder);
            });
        }

        public async Task<IEnumerable<Song>> GetSongsFromGenreAsync(string genreId, SongSortOrder sortOrder = SongSortOrder.Default)
        {
            if (string.IsNullOrWhiteSpace(genreId))
                throw new ArgumentNullException(nameof(genreId));

            return await Task.Run(() =>
            {
                var mediaQuery = PrepareMediaQuery();
                var value = NSObject.FromObject(genreId);
                var genreIdPredicate = MPMediaPropertyPredicate.PredicateWithValue(value, MPMediaItem.GenrePersistentIDProperty);
                mediaQuery.AddFilterPredicate(genreIdPredicate);
                return mediaQuery.ToSongs(sortOrder);
            });
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

            return await Task.Run(() =>
            {
                var mediaQuery = PrepareMediaQuery();
                var value = NSObject.FromObject(searchTerm);
                var searchTermPredicate = MPMediaPropertyPredicate.PredicateWithValue(value, MPMediaItem.TitleProperty, MPMediaPredicateComparison.Contains);
                mediaQuery.AddFilterPredicate(searchTermPredicate);
                return mediaQuery.ToSongs(sortOrder);
            });
        }

        private static MPMediaQuery PrepareMediaQuery()
        {
            var mediaQuery = MPMediaQuery.SongsQuery;
            mediaQuery.GroupingType = MPMediaGrouping.Title;
            var value = NSNumber.FromInt32((int)MPMediaType.Music);
            var predicate = MPMediaPropertyPredicate.PredicateWithValue(value, MPMediaItem.MediaTypeProperty);
            mediaQuery.AddFilterPredicate(predicate);
            return mediaQuery;
        }
    }
}