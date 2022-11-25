using CiderTimeMaui.Models;
using CiderTimeMaui.Services.Interfaces;
using CiderTimeMaui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace CiderTimeMaui.ViewModels
{
    [QueryProperty(nameof(LabelId), "LabelId")]
    public partial class BeveragesViewModel : ObservableObject
    {
        public ObservableCollection<Beverage> Beverages { get; } = new();

        [ObservableProperty]
        Guid labelId;

        [ObservableProperty]
        string labelName;

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
                    {"LabelId", labelId}
                });
        }

        [RelayCommand]
        async Task GoToEditBeverage(Guid beverageId)
        {
            await Shell.Current.GoToAsync(nameof(EditBeveragePage), true,
                new Dictionary<string, object>
                {
                    {"BeverageId", beverageId}
                });
        }

        [RelayCommand]
        async Task GoToEditLabel()
        {
            await Shell.Current.GoToAsync(nameof(EditLabelPage), true,
                new Dictionary<string, object>
                {
                    {"LabelId", labelId}
                });
        }

        public async Task GetBeverages()
        {
            Beverages.Clear();

            var labels = await _storageService.GetDataFromStorage();

            var currentLabel = labels.FirstOrDefault(l => l.Id == LabelId);
            LabelName = currentLabel.Name;

            if (currentLabel.Beverages.Any() is false)
                return;

            foreach(var beverage in currentLabel.Beverages.OrderBy(x => x.Name))
            {
                Beverages.Add(beverage);
            }
        }
    }
}
