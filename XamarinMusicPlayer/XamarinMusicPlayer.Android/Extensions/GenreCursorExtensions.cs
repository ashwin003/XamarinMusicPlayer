using Android.Database;
using Android.Provider;
using System.Collections.Generic;
using XamarinMusicPlayer.Models;

namespace XamarinMusicPlayer.Droid.Extensions
{
    internal static class GenreCursorExtensions
    {
        internal static IEnumerable<Genre> ToGenres(this ICursor cursor)
        {
            var idColumn = cursor.GetColumnIndex(MediaStore.Audio.Genres.InterfaceConsts.Id);
            var nameColumn = cursor.GetColumnIndex(MediaStore.Audio.Genres.InterfaceConsts.Name);

            var genres = new List<Genre>();
            if(cursor.MoveToFirst())
            {
                do
                {
                    var genre = cursor.ToGenre(idColumn, nameColumn);
                    genres.Add(genre);
                } while (cursor.MoveToNext());
            }

            return genres;
        }

        internal static Genre ToGenre(this ICursor cursor, int idColumn, int nameColumn)
        {
            return new Genre
            {
                Id = ulong.Parse(cursor.GetString(idColumn)),
                Name = cursor.GetString(nameColumn)
            };
        }
    }
}