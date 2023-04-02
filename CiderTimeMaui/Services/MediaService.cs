using CiderTimeMaui.Services.Interfaces;

namespace CiderTimeMaui.Services
{
    public class MediaService : IMediaService
    {
        private readonly string _directory = Path.Combine(FileSystem.Current.AppDataDirectory, "media");

        private readonly IPermissionsService _permissionsService;

        public MediaService(IPermissionsService permissionsService) 
        {
            _permissionsService = permissionsService;
        }

        public async Task GetImage(string imageUrl)
        {
            var hasPermission = await _permissionsService.CheckMediaPermissions();
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
            var hasPermission = await _permissionsService.CheckCameraPermissions();
            if (hasPermission is false)
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
    }
}
