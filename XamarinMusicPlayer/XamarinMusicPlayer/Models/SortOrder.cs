namespace XamarinMusicPlayer.Models
{
    public enum AlbumSortOrder { Default, AlphabeticArtistName, MoreSongsFirst, LessSongsFirst, MostRecentYear, OldestYear }

    public enum ArtistSortOrder { Default, MoreAlbumsFirst, LessAlbumsFirst, MoreTracksFirst, LessTracksFirst }

    public enum GenreSortOrder { Default }

    public enum PlaylistSortOrder { Default, NewestFirst, OldestFirst }

    public enum SongSortOrder { Default, AlphabeticComposer, GreaterDuration, SmallerDuration, RecentYear, OldestYear, AlphabeticFirst, AlphabeticAlbum, GreaterTrackNumber, SmallerTrackNumber, DisplayName }
}
