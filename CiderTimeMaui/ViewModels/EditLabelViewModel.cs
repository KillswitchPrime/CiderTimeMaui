using CiderTimeMaui.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CiderTimeMaui.ViewModels
{
    [QueryProperty(nameof(Id), "LabelId")]
    public partial class EditLabelViewModel : ObservableObject
    {
        [ObservableProperty]
        Guid id;

        [ObservableProperty]
        string name;
        [ObservableProperty]
        string description;

        private readonly IDataStorageService _storageService;

        public EditLabelViewModel(IDataStorageService storageService)
        {
            _storageService = storageService;
        }

        [RelayCommand]
        async Task FinishedEditing()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                await Shell.Current.DisplayAlert("Oops!", "Please add a valid Name.", "OK");
                return;
            }

            var labels = await _storageService.GetDataFromStorage();

            foreach(var label in labels.Where(x => x.Id == Id))
            {
                label.Name = Name;
                label.Description = Description;
            }

            await _storageService.WriteDataToStorage(labels);

            await Shell.Current.GoToAsync("..", true);
        }

        [RelayCommand]
        async Task DeleteLabel()
        {
            var answer = await Shell.Current.DisplayAlert("Advarsel!",
                "Er du sikker på at du vil slette produsenten? Dette er en permanent handling.",
                "Ok", "Avbryt");

            if (answer is false)
                return;

            var labels = await _storageService.GetDataFromStorage();

            labels.RemoveAll(x => x.Id == Id);

            await _storageService.WriteDataToStorage(labels);

            await Shell.Current.GoToAsync($"///{nameof(MainPage)}", true);
        }

        public async Task GetData()
        {
            var labels = await _storageService.GetDataFromStorage();

            var label = labels.FirstOrDefault(x => x.Id == Id);

            Name = label.Name;
            Description = label.Description;
        }
    }
}
