using CiderTimeMaui.Services.Interfaces;

namespace CiderTimeMaui.Services {
    public class PermissionsService : IPermissionsService {

        public async Task<bool> CheckStoragePermissions() 
        {
            var hasStoragePermission = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();

            if (hasStoragePermission is PermissionStatus.Granted)
                return true;

            await Shell.Current.DisplayAlert("The app needs permission to access storage",
                "If you do not grant this permission, the app cannot save any data about beverages, labels, etc.",
                "Ok");

            hasStoragePermission = await Permissions.RequestAsync<Permissions.StorageWrite>();
            return hasStoragePermission == PermissionStatus.Granted;
        }

        public async Task<bool> CheckCameraPermissions() 
        {
            var hasCameraPermission = await Permissions.CheckStatusAsync<Permissions.Camera>();

            if (hasCameraPermission is PermissionStatus.Granted)
                return true;

            await Shell.Current.DisplayAlert("The app needs your permission to take photos",
                "The photos are exclusively used when you want to add a picture to each beverage.",
                "Ok");

            hasCameraPermission = await Permissions.RequestAsync<Permissions.StorageWrite>();
            return hasCameraPermission == PermissionStatus.Granted;
        }

        public async Task<bool> CheckMediaPermissions() 
        {
            var hasMediaPermission = await Permissions.CheckStatusAsync<Permissions.Media>();

            if (hasMediaPermission is PermissionStatus.Granted)
                return true;

            await Shell.Current.DisplayAlert("The app needs your permission to access photos",
                "The photos are exclusively used when you want to add a picture to each beverage.",
                "Ok");

            hasMediaPermission = await Permissions.RequestAsync<Permissions.StorageWrite>();
            return hasMediaPermission == PermissionStatus.Granted;
        }
    }
}
