using CiderTimeMaui.Services.Interfaces;
using System.Text;
using System.Text.Json;
using Label = CiderTimeMaui.Models.Label;

namespace CiderTimeMaui.Services
{
    public class DataStorageService : IDataStorageService
    {
        private static readonly string _storageName = "CiderTimeStorageData.txt";
        private readonly string _storagePath = $"{FileSystem.AppDataDirectory}/{_storageName}";

        public async Task<List<Label>> GetDataFromStorage()
        {
            try
            {
                var bytes = await File.ReadAllBytesAsync(_storagePath);
                var data = Encoding.UTF8.GetString(bytes);
                return JsonSerializer.Deserialize<List<Label>>(data);
            }
            catch (Exception)
            {
                using var fileStream = File.Create(_storagePath);
                await fileStream.DisposeAsync();
                return new List<Label>();
            }
        }

        public async Task WriteDataToStorage(List<Label> labels)
        {
            var data = JsonSerializer.Serialize(labels);

            var stream = File.CreateText(_storagePath);
            await stream.WriteLineAsync(data);
            await stream.DisposeAsync();
        }
    }
}
