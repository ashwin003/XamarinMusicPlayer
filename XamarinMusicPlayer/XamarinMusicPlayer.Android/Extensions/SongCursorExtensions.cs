using Android.Database;
using Android.Provider;
using System.Collections.Generic;
using XamarinMusicPlayer.Models;

namespace XamarinMusicPlayer.Droid.Extensions
{
    internal static class SongCursorExtensions
    {
        internal static IEnumerable<Song> ToSongs(this ICursor cursor)
        {
            var idColumn = cursor.GetColumnIndex(MediaStore.Audio.Media.InterfaceConsts.Id);

            var artistColumn = cursor.GetColumnIndex(MediaStore.Audio.Media.InterfaceConsts.ArtistId);

            var albumColumn = cursor.GetColumnIndex(MediaStore.Audio.Media.InterfaceConsts.AlbumId);

            var composerColumn = cursor.GetColumnIndex(MediaStore.Audio.Media.InterfaceConsts.Composer);

            var titleColumn = cursor.GetColumnIndex(MediaStore.Audio.Media.InterfaceConsts.Title);
            var displayNameColumn = cursor.GetColumnIndex(MediaStore.Audio.Media.InterfaceConsts.DisplayName);

            var durationColumn = cursor.GetColumnIndex(MediaStore.Audio.Media.InterfaceConsts.Duration);

            var trackColumn = cursor.GetColumnIndex(MediaStore.Audio.Media.InterfaceConsts.Track);
            var uriColumn = cursor.GetColumnIndex(MediaStore.Audio.Media.InterfaceConsts.Data);
            var sizeColumn = cursor.GetColumnIndex(MediaStore.Audio.Media.InterfaceConsts.Size);

            var yearColumn = cursor.GetColumnIndex(MediaStore.Audio.Media.InterfaceConsts.Year);

            var isMusicColumn = cursor.GetColumnIndex(MediaStore.Audio.Media.InterfaceConsts.IsMusic);
            var isAlarmColumn = cursor.GetColumnIndex(MediaStore.Audio.Media.InterfaceConsts.IsAlarm);
            var isNotificationColumn = cursor.GetColumnIndex(MediaStore.Audio.Media.InterfaceConsts.IsNotification);
            var isPodcastColumn = cursor.GetColumnIndex(MediaStore.Audio.Media.InterfaceConsts.IsPodcast);

            if (cursor.MoveToFirst())
            {
                do
                {
                    var isMusic = cursor.GetInt(isMusicColumn) != 0;
                    if (isMusic)
                    {
                        yield return cursor.ToSong(
                                           idColumn, 
                                           artistColumn, 
                                           albumColumn, 
                                           composerColumn, 
                                           titleColumn, displayNameColumn, 
                                           durationColumn, 
                                           trackColumn, uriColumn, sizeColumn,
                                           yearColumn,
                                           isAlarmColumn, isNotificationColumn, isPodcastColumn);
                    }

                } while (cursor.MoveToNext());
            }

            cursor?.Close();
            cursor?.Dispose();
        }

        private static Song ToSong(this ICursor cursor, 
                                    int idColumn, 
                                    int artistColumn, 
                                    int albumColumn, 
                                    int composerColumn, 
                                    int titleColumn, int displayNameColumn, 
                                    int durationColumn, 
                                    int trackColumn, int uriColumn, int sizeColumn,
                                    int yearColumn,
                                    int isAlarmColumn, int isNotificationColumn, int isPodcastColumn)
        {
            return new Song
            {
                Id = ulong.Parse(cursor.GetString(idColumn)),

                ArtistId = ulong.Parse(cursor.GetString(artistColumn)),
                AlbumId = ulong.Parse(cursor.GetString(albumColumn)),

                Composer = cursor.GetString(composerColumn),

                Title = cursor.GetString(titleColumn),
                DisplayName = cursor.GetString(displayNameColumn),

                Duration = ulong.Parse(cursor.GetString(durationColumn)),

                Track = cursor.GetString(trackColumn),
                Uri = cursor.GetString(uriColumn),
                FileSize = cursor.GetString(sizeColumn),

                Year = cursor.GetInt(yearColumn),

                IsMusic = true,
                IsAlarm = cursor.GetInt(isAlarmColumn) != 0,
                IsNotification = cursor.GetInt(isNotificationColumn) != 0,
                IsPodcast = cursor.GetInt(isPodcastColumn) != 0,
            };
        }
    }
}