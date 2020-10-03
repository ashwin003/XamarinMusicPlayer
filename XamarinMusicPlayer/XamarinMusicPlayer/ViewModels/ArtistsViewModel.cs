using Xamarin.Forms;
using XamarinMusicPlayer.Models;
using XamarinMusicPlayer.ServiceInterfaces;

namespace XamarinMusicPlayer.ViewModels
{
    public class ArtistsViewModel : BaseViewModel<Artist>
    {
        public ArtistsViewModel() : this(DependencyService.Get<IArtistService>())
        {
        }

        public ArtistsViewModel(IArtistService service) : base(() => service.GetArtistsAsync())
        {
            Title = "Artists";
        }
    }
}
