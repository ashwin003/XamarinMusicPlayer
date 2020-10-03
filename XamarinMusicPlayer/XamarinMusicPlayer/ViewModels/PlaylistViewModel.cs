using Xamarin.Forms;
using XamarinMusicPlayer.Models;
using XamarinMusicPlayer.ServiceInterfaces;

namespace XamarinMusicPlayer.ViewModels
{
    public class PlaylistViewModel : BaseViewModel<Playlist>
    {
        public PlaylistViewModel() : this(DependencyService.Get<IPlaylistService>())
        {
        }

        public PlaylistViewModel(IPlaylistService service) : base(() => service.GetPlaylistsAsync())
        {
            Title = "Playlist";
        }
    }
}
