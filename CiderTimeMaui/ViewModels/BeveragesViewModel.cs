using CiderTimeMaui.Models;
using CiderTimeMaui.Services.Interfaces;
using CiderTimeMaui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace CiderTimeMaui.ViewModels
{
    [QueryProperty(nameof(Id), "Id")]
    public partial class BeveragesViewModel : ObservableObject
    {
        public ObservableCollection<Beverage> Beverages { get; } = new();

        [ObservableProperty]
        Guid id;

        private readonly IDataStorageService _storageService;

        public BeveragesViewModel(IDataStorageService storageService)
        {
            _storageService = storageService;
        }

        [RelayCommand]
        async Task GoToAddBeverage()
        {
            await Shell.Current.GoToAsync(nameof(AddBeveragePage), true, 
                new Dictionary<string, object>
                {
                    {"Id", Id}
                });
        }

        public async Task GetBeverages()
        {
            Beverages.Clear();

            var labels = await _storageService.GetDataFromStorage();

            var currentLabel = labels.FirstOrDefault(l => l.Id == Id);

            foreach(var beverage in currentLabel.Beverages)
            {
                Beverages.Add(beverage);
            }
        }
    }
}
