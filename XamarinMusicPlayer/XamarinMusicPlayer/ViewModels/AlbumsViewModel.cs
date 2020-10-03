using Xamarin.Forms;
using XamarinMusicPlayer.Models;
using XamarinMusicPlayer.ServiceInterfaces;

namespace XamarinMusicPlayer.ViewModels
{
    public class AlbumsViewModel : BaseViewModel<Album>
    {
        public AlbumsViewModel() : this(DependencyService.Get<IAlbumService>())
        {
        }

        public AlbumsViewModel(IAlbumService service) : base("Albums", () => service.GetAlbumsAsync())
        {
        }
    }
}
