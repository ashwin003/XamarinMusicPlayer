using Android.Provider;
using XamarinMusicPlayer.Models;

namespace XamarinMusicPlayer.Droid.Extensions
{
    internal static class SortOrderExtensions
    {
        private const string ASC = " ASC";
        private const string DESC = " DESC";

        internal static string ToStringSortOrder(this SongSortOrder sortOrder)
        {
            return sortOrder switch
            {
                SongSortOrder.AlphabeticAlbum => MediaStore.Audio.Media.InterfaceConsts.AlbumKey,
                SongSortOrder.AlphabeticComposer => MediaStore.Audio.Media.InterfaceConsts.Composer + ASC,
                SongSortOrder.DisplayName => MediaStore.Audio.Media.InterfaceConsts.DisplayName,
                SongSortOrder.GreaterDuration => MediaStore.Audio.Media.InterfaceConsts.Duration + DESC,
                SongSortOrder.SmallerDuration => MediaStore.Audio.Media.InterfaceConsts.Duration + ASC,
                SongSortOrder.GreaterTrackNumber => MediaStore.Audio.Media.InterfaceConsts.Track + DESC,
                SongSortOrder.SmallerTrackNumber => MediaStore.Audio.Media.InterfaceConsts.Track + ASC,
                SongSortOrder.OldestYear => MediaStore.Audio.Media.InterfaceConsts.Year + DESC,
                SongSortOrder.RecentYear => MediaStore.Audio.Media.InterfaceConsts.Year + ASC,
                _ => MediaStore.Audio.Media.DefaultSortOrder
            };
        }

        internal static string ToStringSortOrder(this AlbumSortOrder sortOrder)
        {
            return sortOrder switch
            {
                AlbumSortOrder.LessSongsFirst => MediaStore.Audio.AlbumColumns.NumberOfSongs + ASC,
                AlbumSortOrder.MoreSongsFirst => MediaStore.Audio.AlbumColumns.NumberOfSongs + DESC,
                AlbumSortOrder.AlphabeticArtistName => MediaStore.Audio.AlbumColumns.Artist,
                AlbumSortOrder.MostRecentYear => MediaStore.Audio.AlbumColumns.LastYear + DESC,
                AlbumSortOrder.OldestYear => MediaStore.Audio.AlbumColumns.LastYear + ASC,
                _ => MediaStore.Audio.Albums.DefaultSortOrder
            };
        }

        internal static string ToStringSortOrder(this ArtistSortOrder sortOrder)
        {
            return sortOrder switch
            {
                ArtistSortOrder.MoreTracksFirst => MediaStore.Audio.ArtistColumns.NumberOfTracks + DESC,
                ArtistSortOrder.LessTracksFirst => MediaStore.Audio.ArtistColumns.NumberOfTracks + ASC,
                _ => MediaStore.Audio.Artists.DefaultSortOrder
            };
        }

        internal static string ToStringSortOrder(this GenreSortOrder sortOrder)
        {
            return sortOrder switch
            {
                _ => MediaStore.Audio.GenresColumns.Name + ASC
            };
        }

        internal static string ToStringSortOrder(this PlaylistSortOrder sortOrder)
        {
            return sortOrder switch
            {
                PlaylistSortOrder.NewestFirst => MediaStore.Audio.PlaylistsColumns.DateAdded + DESC,
                PlaylistSortOrder.OldestFirst => MediaStore.Audio.PlaylistsColumns.DateAdded + ASC,
                _ => MediaStore.Audio.Playlists.DefaultSortOrder
            };
        }
    }
}