using Android.Database;
using Android.Provider;
using System.Collections.Generic;
using XamarinMusicPlayer.Models;

namespace XamarinMusicPlayer.Droid.Extensions
{
    internal static class AlbumCursorExtensions
    {
        internal static IEnumerable<Album> ToAlbums(this ICursor cursor)
        {
            var idColumn = cursor.GetColumnIndex(MediaStore.Audio.Albums.InterfaceConsts.Id);
            var albumColumn = cursor.GetColumnIndex(MediaStore.Audio.Albums.InterfaceConsts.Album);
            var artistColumn = cursor.GetColumnIndex(MediaStore.Audio.Albums.InterfaceConsts.Artist);
            var numberOfSongsColumn = cursor.GetColumnIndex(MediaStore.Audio.Albums.InterfaceConsts.NumberOfSongs);

            if(cursor.MoveToFirst())
            {
                do
                {
                    yield return cursor.ToAlbum(idColumn, albumColumn, artistColumn, numberOfSongsColumn);
                } while (cursor.MoveToLast());
            }

            cursor?.Close();
            cursor?.Dispose();

        }

        internal static Album ToAlbum(this ICursor cursor, int idColumn, int albumColumn, int artistColumn, int numberOfSongsColumn)
        {
            return new Album
            {
                Id = ulong.Parse(cursor.GetString(idColumn)),
                Title = cursor.GetString(albumColumn),
                Artist = cursor.GetString(artistColumn),
                NumberOfSongs = cursor.GetInt(numberOfSongsColumn)
            };
        }
    }
}