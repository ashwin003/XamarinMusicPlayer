namespace XamarinMusicPlayer.Models
{
    public class Album : DataModel
    {
        public string Title { get; set; }

        public string AlbumArt { get; set; }

        public string Artist { get; set; }

        public int NumberOfSongs { get; set; }
    }
}
