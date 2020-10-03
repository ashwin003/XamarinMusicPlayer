namespace XamarinMusicPlayer.Models
{
    public class Song : DataModel
    {
        public string Title { get; set; }

        public string DisplayName { get; set; }

        public ulong AlbumId { get; set; }

        public string Composer { get; set; }

        public int Year { get; set; }

        public string Track { get; set; }

        public ulong Duration { get; set; }

        public string Uri { get; set; }

        public string FileSize { get; set; }

        public string AlbumArtwork { get; set; }

        public bool IsMusic { get; set; }

        public bool IsAlarm { get; set; }

        public bool IsRingtone { get; set; }

        public bool IsNotification { get; set; }

        public bool IsPodcast { get; set; }

        public ulong ArtistId { get; set; }
    }
}
