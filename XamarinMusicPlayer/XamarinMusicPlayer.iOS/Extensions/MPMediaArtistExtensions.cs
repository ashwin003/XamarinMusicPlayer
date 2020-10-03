using MediaPlayer;
using System.Collections.Generic;
using XamarinMusicPlayer.Models;

namespace XamarinMusicPlayer.iOS.Extensions
{
    internal static class MPMediaArtistExtensions
    {
        internal static IEnumerable<Artist> ToArtists(this MPMediaQuery mediaQuery, ArtistSortOrder sortOrder)
        {
            foreach (var mediaItem in mediaQuery.Items)
            {
                if (mediaItem != null)
                {
                    yield return mediaItem.ToArtist();
                }
            }
        }

        internal static Artist ToArtist(this MPMediaItem mediaItem)
        {
            return new Artist
            {
                Id = mediaItem.ArtistPersistentID,
                Name = mediaItem.Artist
            };
        }
    }
}