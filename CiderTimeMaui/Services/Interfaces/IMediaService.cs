namespace CiderTimeMaui.Services.Interfaces
{
    public interface IMediaService
    {
        Task TakePhoto(string imageUrl);
        Task GetImage(string imageUrl);
    }
}
