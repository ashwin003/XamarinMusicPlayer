using System.Collections.Generic;

namespace XamarinMusicPlayer.ServiceInterfaces
{
    public interface IPlaylistManager
    {
        bool CreatePlaylist(string name);

        bool AddSongToPlaylist(ulong playlistId, ulong songId);

        bool RemoveSongFromPlaylist(ulong playlistId, ulong songId);

        bool UpdatePlaylistOrder(ulong playlistId, IEnumerable<ulong> songIds);

        bool DeletePlaylist(ulong id);
    }
}
