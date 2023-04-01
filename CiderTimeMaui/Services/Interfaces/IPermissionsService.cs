namespace CiderTimeMaui.Services.Interfaces {
    public interface IPermissionsService {
        Task<bool> CheckStoragePermissions();
        Task<bool> CheckCameraPermissions();
        Task<bool> CheckMediaPermissions();
    }
}