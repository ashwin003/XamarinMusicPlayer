using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinMusicPlayer.Models;

namespace XamarinMusicPlayer.ServiceInterfaces
{
    public interface IArtistService
    {
        Task<IEnumerable<Artist>> GetArtistsAsync(ArtistSortOrder sortOrder = ArtistSortOrder.Default);

        Task<IEnumerable<Artist>> GetArtistsByIdAsync(IEnumerable<string> artistIds, ArtistSortOrder sortOrder = ArtistSortOrder.Default);

        Task<IEnumerable<Artist>> SearchArtistsAsync(string searchTerm, ArtistSortOrder sortOrder = ArtistSortOrder.Default);

        Task<IEnumerable<Artist>> GetArtistsByGenre(string genreId, ArtistSortOrder sortOrder = ArtistSortOrder.Default);
    }
}
