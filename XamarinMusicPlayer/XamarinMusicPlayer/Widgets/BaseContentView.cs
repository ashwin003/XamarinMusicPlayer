using Xamarin.Forms;
using XamarinMusicPlayer.Models;

namespace XamarinMusicPlayer.Widgets
{
    public class BaseContentView<T> : ContentView where T: class
    {
        public static readonly BindableProperty ItemProperty = BindableProperty.Create(
                propertyName: nameof(Item),
                returnType: typeof(T),
                declaringType: typeof(BaseContentView<T>));

        private T _item;
        public T Item
        {
            get
            {
                return _item;
            }
            set
            {
                _item = value;
                OnPropertyChanged();
            }
        }
    }

    public class PlaylistBaseContentView : BaseContentView<Playlist> { }

    public class ArtistBaseContentView : BaseContentView<Artist> { }

    public class AlbumBaseContentView : BaseContentView<Album> { }

    public class SongBaseContentView : BaseContentView<Song> { }

    public class GenreBaseContentView : BaseContentView<Genre> { }
}
