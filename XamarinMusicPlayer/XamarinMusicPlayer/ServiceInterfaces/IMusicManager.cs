using System;
using System.Collections.Generic;
using System.Text;
using XamarinMusicPlayer.Models;

namespace XamarinMusicPlayer.ServiceInterfaces
{
    public interface IMusicManager
    {
        bool Play(Song song);

        bool Pause();

        bool Stop();

        bool SkipToNext();

        bool SkipTo(int milliseconds);

        bool AddToQueue(Song song);

        bool UpdateQueue(IEnumerable<Song> songs);

        bool PlayNext(Song song);
    }
}
