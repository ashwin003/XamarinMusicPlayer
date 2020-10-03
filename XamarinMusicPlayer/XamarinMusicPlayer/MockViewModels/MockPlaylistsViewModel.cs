using System.Collections.ObjectModel;
using System.Threading.Tasks;
using XamarinMusicPlayer.MockProviders;
using XamarinMusicPlayer.Models;
using XamarinMusicPlayer.ViewModels;

namespace XamarinMusicPlayer.MockViewModels
{
    public class MockPlaylistsViewModel : BaseViewModel<Playlist>
    {
        public MockPlaylistsViewModel() : base(() => Task.FromResult(PlaylistHelper.Playlists))
        {
        }
    }
}
