using Android.App;
using Android.Content;
using Android.Database;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Nio.Channels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XamarinMusicPlayer.Models;

namespace XamarinMusicPlayer.Droid.Extensions
{
    internal static class PlaylistCursorExtensions
    {
        internal static IEnumerable<Playlist> ToPlaylists(this ICursor cursor)
        {
            var idColumn = cursor.GetColumnIndex(MediaStore.Audio.Playlists.InterfaceConsts.Id);
            var nameColumn = cursor.GetColumnIndex(MediaStore.Audio.Playlists.InterfaceConsts.Name);
            var dateAddedColumn = cursor.GetColumnIndex(MediaStore.Audio.PlaylistsColumns.DateAdded);

            if (cursor.MoveToFirst())
            {
                do
                {
                    yield return cursor.ToPlaylist(idColumn, nameColumn, dateAddedColumn);
                } while (cursor.MoveToNext());
            }

            cursor?.Close();
            cursor?.Dispose();
        }

        internal static Playlist ToPlaylist(this ICursor cursor, int idColumn, int nameColumn, int dateAddedColumn)
        {
            var time = cursor.GetLong(dateAddedColumn);
            var playlistId = ulong.Parse(cursor.GetString(idColumn));
            return new Playlist
            {
                Id = playlistId,
                Name = cursor.GetString(nameColumn),
                CreationDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(time),
                MemberIds = GetPlaylistMembers(playlistId).ToList()
            };
        }

        private static IEnumerable<string> GetPlaylistMembers(ulong playlistId)
        {
            var memberIds = new List<string>();
            const string projection = MediaStore.Audio.Playlists.Members.AudioId;
            using var cursor = Application.Context.ContentResolver.Query(
                                uri: MediaStore.Audio.Playlists.Members.GetContentUri("external", (long)playlistId),
                                projection: new[] { projection },
                                selection: null,
                                selectionArgs: null,
                                sortOrder: MediaStore.Audio.Playlists.Members.DefaultSortOrder
                             );

            var memberIdColumn = cursor.GetColumnIndex(projection);
            if(cursor.MoveToFirst())
            {
                do
                {
                    var memberId = cursor.GetString(memberIdColumn);
                    memberIds.Add(memberId);
                } while (cursor.MoveToNext());
            }

            return memberIds;
        }
    }
}