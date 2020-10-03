using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinMusicPlayer.Widgets
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GenreContentView : GenreBaseContentView
    {
        public GenreContentView()
        {
            InitializeComponent();
        }
    }
}