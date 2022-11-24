using CiderTimeMaui.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CiderTimeMaui.ViewModels
{
    [QueryProperty(nameof(Id), "BeverageId")]
    public partial class EditBeverageViewModel : ObservableObject
    {
        [ObservableProperty]
        Guid id;

        [ObservableProperty]
        string name;
        [ObservableProperty]
        string description;
        [ObservableProperty]
        string rating;
        [ObservableProperty]
        string price;

        private readonly IDataStorageService _storageService;

        public EditBeverageViewModel(IDataStorageService storageService)
        {
            _storageService = storageService;
        }

        [RelayCommand]
        async Task FinishedEditing()
        {
            var labels = await _storageService.GetDataFromStorage();

            foreach (var beverage in labels.SelectMany(l => l.Beverages).Where(b => b.Id == Id))
            {
                beverage.Name = Name;
                beverage.Description = Description;
                beverage.Rating = int.Parse(Rating);
                beverage.Price = decimal.Parse(Price);
            }

            await _storageService.WriteDataToStorage(labels);

            var labelId = labels.FirstOrDefault(l => l.Beverages.Any(b => b.Id == Id)).Id;
            await Shell.Current.GoToAsync("..", true,
                new Dictionary<string, object>
                {
                    {"LabelId", labelId}
                });
        }

        public async Task GetData()
        {
            var labels = await _storageService.GetDataFromStorage();

            var beverage = labels
                .SelectMany(l => l.Beverages)
                .FirstOrDefault(b => b.Id == Id);

            Name = beverage.Name;
            Description = beverage.Description;
            Rating = beverage.Rating.ToString();
            Price = beverage.Price.ToString();
        }
    }
}
