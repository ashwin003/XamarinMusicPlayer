using MediaPlayer;
using System.Collections.Generic;
using XamarinMusicPlayer.Models;

namespace XamarinMusicPlayer.iOS.Extensions
{
    internal static class MPMediaSongExtensions
    {
        internal static IEnumerable<Song> ToSongs(this MPMediaQuery mediaQuery, SongSortOrder sortOrder)
        {
            foreach (var mediaItem in mediaQuery.Items)
            {
                if(mediaItem != null && mediaItem.AssetURL != null)
                {
                    yield return mediaItem.ToSong();
                }
            }
        }

        internal static Song ToSong(this MPMediaItem mediaItem)
        {
            return new Song
            {
                Id = mediaItem.PersistentID,
                Title = mediaItem.Title,
                DisplayName = mediaItem.Title,
                ArtistId = mediaItem.AlbumArtistPersistentID,
                AlbumId = mediaItem.AlbumPersistentID,
                Composer = mediaItem.Composer,
                Duration = (ulong)mediaItem.PlaybackDuration,
                Uri = mediaItem.AssetURL.AbsoluteString,
                Track = $"{mediaItem.AlbumTrackNumber}/{mediaItem.AlbumTrackCount}",
            }
        }
    }
}