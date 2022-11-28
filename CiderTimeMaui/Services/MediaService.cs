using CiderTimeMaui.Services.Interfaces;

namespace CiderTimeMaui.Services
{
    public class MediaService : IMediaService
    {
        private readonly string _directory = $"{FileSystem.AppDataDirectory}/Media";

        public async Task GetImage(string imageUrl)
        {
            var hasPermission = await CheckPermissions();

            if (hasPermission is false)
                return;

            var image = await MediaPicker.Default.PickPhotoAsync();

            if (image is null)
                return;

            using var imageStream = await image.OpenReadAsync();

            if(Directory.Exists(_directory) is false) 
                Directory.CreateDirectory(_directory);

            using var fileStream = File.OpenWrite(imageUrl);
            await imageStream.CopyToAsync(fileStream);

            await imageStream.DisposeAsync();
            await fileStream.DisposeAsync();
        }

        public async Task TakePhoto(string imageUrl)
        {
            var hasPermission = await CheckPermissions();

            if(hasPermission is false) 
                return;

            var photo = await MediaPicker.Default.CapturePhotoAsync();

            if(photo is null)
                return;

            using var photoStream = await photo.OpenReadAsync();

            if (Directory.Exists(_directory) is false)
                Directory.CreateDirectory(_directory);

            using var fileStream = File.OpenWrite(imageUrl);

            await photoStream.CopyToAsync(fileStream);

            await photoStream.DisposeAsync();
            await fileStream.DisposeAsync();
        }

        private static async Task<bool> CheckPermissions()
        {
            var hasPermission = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();

            if (hasPermission is PermissionStatus.Granted)
                return true;

            return await Permissions.RequestAsync<Permissions.StorageWrite>() == PermissionStatus.Granted;
        }
    }
}
