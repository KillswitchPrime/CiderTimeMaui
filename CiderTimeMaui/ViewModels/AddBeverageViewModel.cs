using CiderTimeMaui.Models;
using CiderTimeMaui.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CiderTimeMaui.ViewModels
{
    [QueryProperty(nameof(LabelId), "LabelId")]
    public partial class AddBeverageViewModel : ObservableObject
    {
        [ObservableProperty]
        Guid labelId;

        [ObservableProperty]
        string name;
        [ObservableProperty]
        string description;
        [ObservableProperty]
        string price;
        [ObservableProperty]
        string rating;
        [ObservableProperty]
        object image = null;

        private readonly IDataStorageService _storageService;

        public AddBeverageViewModel(IDataStorageService storageService)
        {
            _storageService = storageService;
        }

        [RelayCommand]
        async Task AddBeverage()
        {
            var beverage = new Beverage
            {
                Id = Guid.NewGuid(),
                Name = Name,
                Description = Description,
                Price = decimal.Parse(Price),
                Rating = int.Parse(Rating),
                Image = nameof(Image)
            };

            var labels = await _storageService.GetDataFromStorage();

            foreach(var label in labels.Where(x => x.Id == LabelId))
            {
                label.Beverages.Add(beverage);
            }

            await _storageService.WriteDataToStorage(labels);

            await Shell.Current.GoToAsync("..", true);
        }
    }
}
