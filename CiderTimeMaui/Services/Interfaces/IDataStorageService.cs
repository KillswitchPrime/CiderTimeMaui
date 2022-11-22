namespace CiderTimeMaui.Services.Interfaces
{
    public interface IDataStorageService
    {
        Task<string> GetDataFromStorage();
        Task WriteDataToStorage(string data);
    }
}
