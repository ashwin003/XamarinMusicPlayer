using MediaPlayer;
using System.Collections.Generic;
using XamarinMusicPlayer.Models;

namespace XamarinMusicPlayer.iOS.Extensions
{
    internal static class MPMediaAlbumExtensions
    {
        internal static IEnumerable<Album> ToAlbums(this MPMediaQuery mediaQuery, AlbumSortOrder sortOrder)
        {
            foreach (var mediaItem in mediaQuery.Items)
            {
                if (mediaItem != null)
                {
                    yield return mediaItem.ToAlbum();
                }
            }
        }

        internal static Album ToAlbum(this MPMediaItem mediaItem)
        {
            return new Album
            {
                Id = mediaItem.PersistentID,
                Title = mediaItem.AlbumTitle,
                Artist = mediaItem.AlbumArtist,
                NumberOfSongs = mediaItem.AlbumTrackCount
            };
        }
    }
}