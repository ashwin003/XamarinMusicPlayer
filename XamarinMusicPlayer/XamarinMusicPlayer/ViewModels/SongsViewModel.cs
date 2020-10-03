using Xamarin.Forms;
using XamarinMusicPlayer.Models;
using XamarinMusicPlayer.ServiceInterfaces;

namespace XamarinMusicPlayer.ViewModels
{
    public class SongsViewModel : BaseViewModel<Song>
    {
        public SongsViewModel() : this(DependencyService.Get<ISongService>())
        {
        }

        public SongsViewModel(ISongService service) : base("Songs", () => service.GetSongsAsync())
        {
        }
    }
}
