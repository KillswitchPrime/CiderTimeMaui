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

        [RelayCommand]
        async Task Search(string searchQuery)
        {
            if (string.IsNullOrEmpty(searchQuery) || Beverages.Any() is false)
            {
                await GetBeverages();
                return;
            }

            var formattedSearchQuery = searchQuery.ToUpper().Trim();

            var labels = await _storageService.GetDataFromStorage();

            var beverages = labels.FirstOrDefault(l => l.Id == LabelId).Beverages;

            var searchedBeverages = beverages
                .Where(b => 
                    b.Name.ToUpper().Contains(formattedSearchQuery) || 
                    (!string.IsNullOrWhiteSpace(b.Description) && b.Description.ToUpper().Contains(formattedSearchQuery)))
                .ToList();

            Beverages.Clear();

            foreach (var beverage in searchedBeverages)
                Beverages.Add(beverage);
        }

        [RelayCommand]
        async Task ResetSearch()
        {
            await GetBeverages();
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
                Beverages.Add(beverage);
        }

        public void SortBeveragesList(int sortType)
        {
            var sortedBeverages = sortType switch
            {
                0 => Beverages.OrderBy(b => b.Name).ToList(),
                1 => Beverages.OrderByDescending(b => b.Name).ToList(),
                2 => Beverages.OrderByDescending(b => b.Rating).ToList(),
                3 => Beverages.OrderBy(b => b.Rating).ToList(),
                4 => Beverages.OrderByDescending(b => b.Price).ToList(),
                5 => Beverages.OrderBy(b => b.Price).ToList(),
                _ => Beverages.OrderBy(b => b.Name).ToList()
            };

            Beverages.Clear();

            foreach (var beverage in sortedBeverages.ToList())
                Beverages.Add(beverage);
        }
    }
}
