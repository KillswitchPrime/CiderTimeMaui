using Label = CiderTimeMaui.Models.Label;

namespace CiderTimeMaui.Services.Interfaces
{
    public interface IDataStorageService
    {
        Task<List<Label>> GetDataFromStorage();
        Task WriteDataToStorage(List<Label> data);
    }
}
