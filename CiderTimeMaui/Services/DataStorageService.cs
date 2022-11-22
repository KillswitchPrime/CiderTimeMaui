using CiderTimeMaui.Services.Interfaces;
using System.Text;

namespace CiderTimeMaui.Services
{
    public class DataStorageService : IDataStorageService
    {
        private static readonly string _storageName = "CiderTimeStorageData.txt";
        private readonly string _storagPath = $"{FileSystem.AppDataDirectory}/{_storageName}";

        public async Task<string> GetDataFromStorage()
        {
            var bytes = await File.ReadAllBytesAsync(_storagPath);
            return Encoding.UTF8.GetString(bytes);
        }

        public async Task WriteDataToStorage(string data)
        {
            var stream = File.OpenWrite(_storagPath);
            await stream.WriteAsync(Encoding.UTF8.GetBytes(data));
        }
    }
}
