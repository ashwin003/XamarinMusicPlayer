using Android.Database;
using Android.Provider;
using System.Collections.Generic;
using XamarinMusicPlayer.Models;

namespace XamarinMusicPlayer.Droid.Extensions
{
    internal static class ArtistCursorExtensions
    {
       
        internal static IEnumerable<Artist> ToArtists(this ICursor cursor)
        {
            var idColumn = cursor.GetColumnIndex(MediaStore.Audio.Artists.InterfaceConsts.Id);
            var artistColum = cursor.GetColumnIndex(MediaStore.Audio.Artists.InterfaceConsts.Artist);
            var numberOfTracksColumn = cursor.GetColumnIndex(MediaStore.Audio.Artists.InterfaceConsts.NumberOfTracks);
            var numberofAlbumsColumn = cursor.GetColumnIndex(MediaStore.Audio.Artists.InterfaceConsts.NumberOfAlbums);

            if(cursor.MoveToFirst())
            {
                do
                {
                    yield return cursor.ToArtist(idColumn, artistColum, numberOfTracksColumn, numberofAlbumsColumn);
                } while (cursor.MoveToNext());
            }

            cursor?.Close();
            cursor?.Dispose();
        }

        internal static Artist ToArtist(this ICursor cursor, int idColumn, int nameColumn, int numberOfTracksColumn, int numberOfAlbumsColumn)
        {
            return new Artist
            {
                Id = ulong.Parse(cursor.GetString(idColumn)),
                Name = cursor.GetString(nameColumn),
                NumberOfAlbums = numberOfAlbumsColumn != -1 ? cursor.GetInt(numberOfAlbumsColumn) : 0,
                NumberOfTracks = numberOfTracksColumn != -1 ? cursor.GetInt(numberOfTracksColumn) : 0
            };
        }
    }
}