using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinMusicPlayer.Models;

namespace XamarinMusicPlayer.ServiceInterfaces
{
    public interface IPlaylistService
    {
        Task<IEnumerable<Playlist>> GetPlaylistsAsync(PlaylistSortOrder sortOrder = PlaylistSortOrder.Default);

        Task<IEnumerable<Playlist>> SearchPlaylistsAsync(string searchTerm, PlaylistSortOrder sortOrder = PlaylistSortOrder.Default);
    }
}
