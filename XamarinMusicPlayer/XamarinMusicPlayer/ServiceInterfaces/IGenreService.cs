using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinMusicPlayer.Models;

namespace XamarinMusicPlayer.ServiceInterfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<Genre>> GetGenresAsync(GenreSortOrder sortOrder = GenreSortOrder.Default);

        Task<IEnumerable<Genre>> SearchGenresAsync(string searchTerm, GenreSortOrder sortOrder = GenreSortOrder.Default);
    }
}
