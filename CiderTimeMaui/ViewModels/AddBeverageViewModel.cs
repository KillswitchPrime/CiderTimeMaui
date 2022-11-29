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
        Guid imageId = Guid.NewGuid();

        private readonly IDataStorageService _storageService;
        private readonly IMediaService _mediaService;
        private readonly string _imageUrl = $"{FileSystem.AppDataDirectory}/Media";

        public AddBeverageViewModel(IDataStorageService storageService,
            IMediaService mediaService)
        {
            _storageService = storageService;
            _mediaService = mediaService;
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
                ImageUrl = $"{_imageUrl}/{ImageId}.jpg"
            };

            var labels = await _storageService.GetDataFromStorage();

            foreach(var label in labels.Where(x => x.Id == LabelId))
                label.Beverages.Add(beverage);

            await _storageService.WriteDataToStorage(labels);

            await Shell.Current.GoToAsync("..", true);
        }

        [RelayCommand]
        async Task GetImage()
        {
            await _mediaService.GetImage($"{_imageUrl}/{ImageId}");
        }

        [RelayCommand]
        async Task TakePhoto()
        {
            await _mediaService.TakePhoto($"{_imageUrl}/{ImageId}");
        }
    }
}
