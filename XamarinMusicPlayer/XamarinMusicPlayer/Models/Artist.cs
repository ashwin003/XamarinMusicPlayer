namespace XamarinMusicPlayer.Models
{
    public class Artist : DataModel
    {
        public string Name { get; set; }

        public int NumberOfTracks { get; set; }

        public int NumberOfAlbums { get; set; }

        public string Artpath { get; set; }
    }
}
