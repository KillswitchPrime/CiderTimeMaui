using CiderTimeMaui.Models;
using CiderTimeMaui.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text.Json;
using Label = CiderTimeMaui.Models.Label;

namespace CiderTimeMaui.ViewModels
{
    public partial class AddLabelViewModel(IDataStorageService storageService) : ObservableObject
    {
        [ObservableProperty]
        string name;
        [ObservableProperty]
        string description;
        
        [RelayCommand]
        async Task AddLabel()
        {
            if(string.IsNullOrWhiteSpace(Name))
            {
                await Shell.Current.DisplayAlert("Oops!", "Please add a valid Name.", "OK");
                return;
            }

            var label = new Label
            {
                Id = Guid.NewGuid(),
                Name = Name,
                Description = Description,
                Beverages = new List<Beverage>()
            };

            var labels = await storageService.GetDataFromStorage();

            labels.Add(label);

            await storageService.WriteDataToStorage(labels);

            await Shell.Current.GoToAsync($"///{nameof(MainPage)}", true);
        }
    }
}
