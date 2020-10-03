using Xamarin.Forms;
using XamarinMusicPlayer.Models;
using XamarinMusicPlayer.ServiceInterfaces;

namespace XamarinMusicPlayer.ViewModels
{
    public class GenresViewModel : BaseViewModel<Genre>
    {
        public GenresViewModel() : this(DependencyService.Get<IGenreService>())
        {
        }

        public GenresViewModel(IGenreService service) : base(() => service.GetGenresAsync())
        {
            Title = "Genres";
        }
    }
}
