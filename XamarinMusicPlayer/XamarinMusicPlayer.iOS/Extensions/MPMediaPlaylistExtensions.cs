using Foundation;
using MediaPlayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using UIKit;
using XamarinMusicPlayer.Models;

namespace XamarinMusicPlayer.iOS.Extensions
{
    internal static class MPMediaPlaylistExtensions
    {
        internal static IEnumerable<Playlist> ToPlaylists(this MPMediaQuery mediaQuery, PlaylistSortOrder sortOrder)
        {
            foreach (var mediaItem in mediaQuery.Items)
            {
                if (mediaItem != null && mediaItem.AssetURL != null)
                {
                    yield return mediaItem.ToPlaylist();
                }
            }
        }

        internal static Playlist ToPlaylist(this MPMediaItem mediaItem)
        {
            return new Playlist
            {
                Id = mediaItem.PersistentID,
                Name = mediaItem.Title,
                CreationDate = (DateTime)mediaItem.DateAdded,
                MemberIds = GetMemberIds(mediaItem.PersistentID)
            };
        }

        private static List<string> GetMemberIds(ulong playlistId)
        {
            var mediaQuery = MPMediaQuery.SongsQuery;
            var value = NSNumber.FromUInt64(playlistId);
            var predicate = MPMediaPropertyPredicate.PredicateWithValue(value, MPMediaPlaylistProperty.PersistentID);
            mediaQuery.AddFilterPredicate(predicate);

            return mediaQuery.Items.Where(i => i != null && i.AssetURL != null).Select(i => i.PersistentID.ToString()).ToList();
        }
    }
}