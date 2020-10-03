using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace XamarinMusicPlayer.Converters
{
    public class SongCountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var memberIds = value as IEnumerable<string>;
            if(memberIds != null)
            {
                if(!memberIds.Any())
                {
                    return "No Songs";
                }
                var count = memberIds.Count();
                if(count == 1)
                {
                    return "1 Song";
                }
                return $"{count} Songs";
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
