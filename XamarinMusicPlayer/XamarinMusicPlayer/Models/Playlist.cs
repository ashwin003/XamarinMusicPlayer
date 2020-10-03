using System;
using System.Collections.Generic;

namespace XamarinMusicPlayer.Models
{
    public class Playlist : DataModel
    {
        public string Name { get; set; }

        public List<string> MemberIds { get; set; }

        public DateTime CreationDate { get; set; }

        public string SongCount
        {
            get
            {
                var count = MemberIds?.Count;
                if(count == 0)
                {
                    return "No Songs";
                }
                else if (count == 1)
                {
                    return "1 Song";
                }
                return $"{count} Songs";
            }
        }
    }
}
