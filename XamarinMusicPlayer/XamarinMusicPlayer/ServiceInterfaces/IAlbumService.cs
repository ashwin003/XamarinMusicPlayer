using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinMusicPlayer.Models;

namespace XamarinMusicPlayer.ServiceInterfaces
{
    public interface IAlbumService
    {
        Task<IEnumerable<Album>> GetAlbumsAsync(AlbumSortOrder sortOrder = AlbumSortOrder.Default);

        Task<IEnumerable<Album>> GetAlbumsByIdAsync(IEnumerable<string> albumIds, AlbumSortOrder sortOrder = AlbumSortOrder.Default);

        Task<IEnumerable<Album>> GetAlbumsByGenreAsync(string genreId, AlbumSortOrder sortOrder = AlbumSortOrder.Default);

        Task<IEnumerable<Album>> SearchAlbumsAsync(string searchTerm, AlbumSortOrder sortOrder = AlbumSortOrder.Default);
    }
}
