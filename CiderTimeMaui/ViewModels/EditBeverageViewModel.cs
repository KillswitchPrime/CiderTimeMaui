using CiderTimeMaui.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CiderTimeMaui.ViewModels
{
    [QueryProperty(nameof(Id), "BeverageId")]
    public partial class EditBeverageViewModel(IDataStorageService storageService, IMediaService mediaService)
        : ObservableObject
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
        [ObservableProperty]
        string imageUrl;

        [RelayCommand]
        async Task FinishedEditing()
        {
            var ratingIsValid = int.TryParse(Rating, out var parsedRating);
            if (string.IsNullOrWhiteSpace(Name) ||
                ratingIsValid is false ||
                parsedRating < 0 ||
                parsedRating > 10)
            {
                await Shell.Current.DisplayAlert("Oops!", "Please add a valid Name and Rating.", "OK");
                return;
            }

            var labels = await storageService.GetDataFromStorage();

            foreach (var beverage in labels.SelectMany(l => l.Beverages).Where(b => b.Id == Id))
            {
                beverage.Name = Name;
                beverage.Description = Description;
                beverage.Rating = parsedRating;
                beverage.Price = decimal.TryParse(Price, out var parsedPrice) ? parsedPrice : 0M;
            }

            await storageService.WriteDataToStorage(labels);

            var labelId = labels.FirstOrDefault(l => l.Beverages.Any(b => b.Id == Id)).Id;
            await Shell.Current.GoToAsync("..", true,
                new Dictionary<string, object>
                {
                    {"LabelId", labelId}
                });
        }

        [RelayCommand]
        async Task GetImage()
        {
            await mediaService.GetImage(ImageUrl);
        }

        [RelayCommand]
        async Task TakePhoto()
        {
            await mediaService.TakePhoto(ImageUrl);
        }

        [RelayCommand]
        async Task DeleteBeverage()
        {
            var answer = await Shell.Current.DisplayAlert("Advarsel!",
                "Er du sikker på at du vil slette drinken? Dette er en permanent handling.",
                "Ok", "Avbryt");

            if (answer is false)
                return;

            var labels = await storageService.GetDataFromStorage();
            var labelId = labels.FirstOrDefault(l => l.Beverages.Any(b => b.Id == Id)).Id;

            labels.ForEach(l => l.Beverages.RemoveAll(b => b.Id == Id));

            await storageService.WriteDataToStorage(labels);

            await Shell.Current.GoToAsync("..", true,
                new Dictionary<string, object>
                {
                    { "LabelId", labelId}
                });
        }

        public async Task GetData()
        {
            var labels = await storageService.GetDataFromStorage();

            var beverage = labels
                .SelectMany(l => l.Beverages)
                .FirstOrDefault(b => b.Id == Id);

            Name = beverage.Name;
            Description = beverage.Description;
            Rating = beverage.Rating.ToString();
            Price = beverage.Price.ToString();
            ImageUrl = beverage.ImageUrl;
        }

    }
}
