using System.Collections.Generic;
using System.Linq;

namespace XamarinMusicPlayer.iOS.Extensions
{
    internal static class ListExtensions
    {
        internal static bool HasValue<T>(this IEnumerable<T> list)
        {
            return list != null && list.Any();
        }
    }
}