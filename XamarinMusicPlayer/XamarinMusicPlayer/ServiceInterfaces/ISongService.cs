using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinMusicPlayer.Models;

namespace XamarinMusicPlayer.ServiceInterfaces
{
    public interface ISongService
    {
        Task<IEnumerable<Song>> GetSongsAsync(SongSortOrder sortOrder = SongSortOrder.Default);

        Task<IEnumerable<Song>> GetSongsByIdAsync(IEnumerable<string> songIds, SongSortOrder sortOrder = SongSortOrder.Default);

        Task<IEnumerable<Song>> SearchSongsAsync(string searchTerm, SongSortOrder sortOrder = SongSortOrder.Default);

        Task<IEnumerable<Song>> GetSongsFromPlaylistAsync(Playlist playlist, SongSortOrder sortOrder = SongSortOrder.Default);

        Task<IEnumerable<Song>> GetSongsFromAlbumAsync(string albumId, SongSortOrder sortOrder = SongSortOrder.Default);

        Task<IEnumerable<Song>> GetSongsFromArtistAsync(string artistId, SongSortOrder sortOrder = SongSortOrder.Default);

        Task<IEnumerable<Song>> GetSongsFromGenreAsync(string genreId, SongSortOrder sortOrder = SongSortOrder.Default);
    }
}
