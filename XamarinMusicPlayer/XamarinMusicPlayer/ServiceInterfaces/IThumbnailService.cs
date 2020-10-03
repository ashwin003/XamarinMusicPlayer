using XamarinMusicPlayer.Models;

namespace XamarinMusicPlayer.ServiceInterfaces
{
    public interface IThumbnailService
    {
        byte[] LoadImage(Size size, ulong resourceId, ResourceType resourceType);
    }
}
