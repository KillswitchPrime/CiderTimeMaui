using CiderTimeMaui.Services.Interfaces;
using System.Text;

namespace CiderTimeMaui.Services
{
    public class DataStorageService : IDataStorageService
    {
        private static readonly string _storageName = "CiderTimeStorageData.txt";
        private readonly string _storagePath = $"{FileSystem.AppDataDirectory}/{_storageName}";

        public async Task<string> GetDataFromStorage()
        {
            try
            {
                var bytes = await File.ReadAllBytesAsync(_storagePath);
                return Encoding.UTF8.GetString(bytes);
            }
            catch (FileNotFoundException)
            {
                File.Create(_storagePath);
                return string.Empty;
            }
        }

        public async Task WriteDataToStorage(string data)
        {
            var stream = File.OpenWrite(_storagePath);
            await stream.WriteAsync(Encoding.UTF8.GetBytes(data));
            stream.Close();
        }
    }
}
