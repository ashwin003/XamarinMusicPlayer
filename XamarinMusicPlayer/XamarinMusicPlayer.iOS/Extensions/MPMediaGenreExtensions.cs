using MediaPlayer;
using System.Collections.Generic;
using XamarinMusicPlayer.Models;

namespace XamarinMusicPlayer.iOS.Extensions
{
    internal static class MPMediaGenreExtensions
    {
        internal static IEnumerable<Genre> ToGenres(this MPMediaQuery mediaQuery, GenreSortOrder sortOrder)
        {
            foreach (var mediaItem in mediaQuery.Items)
            {
                if (mediaItem != null)
                {
                    yield return mediaItem.ToGenre();
                }
            }
        }

        internal static Genre ToGenre(this MPMediaItem mediaItem)
        {
            return new Genre
            {
                Id = mediaItem.GenrePersistentID,
                Name = mediaItem.Genre,
            };
        }
    }
}