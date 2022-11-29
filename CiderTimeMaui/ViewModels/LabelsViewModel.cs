using CiderTimeMaui.Services.Interfaces;
using CiderTimeMaui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Label = CiderTimeMaui.Models.Label;

namespace CiderTimeMaui.ViewModels
{
    public partial class LabelsViewModel : ObservableObject
    {
        public ObservableCollection<Label> Labels { get; } = new();
        private readonly IDataStorageService _dataStorageService;

        public LabelsViewModel(IDataStorageService dataStorageService)
        {
            _dataStorageService = dataStorageService;
        }

        public async Task GetData()
        {
            Labels.Clear();

            var labels = await _dataStorageService.GetDataFromStorage();

            foreach (var label in labels.OrderBy(x => x.Name))
            {
                if (label is null)
                    continue;

                Labels.Add(label);
            }
        }

        public void SortLabelList(int sortType)
        {
            if (Labels.Any() is false)
                return;

            var sortedLabels = sortType switch
            {
                0 => Labels.OrderBy(l => l.Name).ToList(),
                1 => Labels.OrderByDescending(l => l.Name).ToList(),
                2 => Labels.OrderByDescending(l => l.Beverages.Count).ToList(),
                3 => Labels.OrderBy(l => l.Beverages.Count).ToList(),
                4 => Labels.OrderByDescending(l => l.Beverages.Any() ? l.Beverages.MaxBy(b => b.Rating).Rating : int.MinValue).ToList(),
                5 => Labels.OrderBy(l => l.Beverages.Any() ? l.Beverages.MinBy(b => b.Rating).Rating : int.MaxValue).ToList(),
                6 => Labels.OrderByDescending(l => l.Beverages.Any() ? l.Beverages.MaxBy(b => b.Price).Price : int.MinValue).ToList(),
                7 => Labels.OrderBy(l => l.Beverages.Any() ? l.Beverages.MinBy(b => b.Price).Price : int.MaxValue).ToList(),
                _ => Labels.OrderBy(l => l.Name).ToList()
            };

            Labels.Clear();

            foreach (var label in sortedLabels.ToList())
                Labels.Add(label);
        }

        [RelayCommand]
        async Task Search(string searchQuery)
        {
            if (string.IsNullOrEmpty(searchQuery) || Labels.Any() is false)
            {
                await GetData();
                return;
            }

            var formattedSearchQuery = searchQuery.ToUpper().Trim();

            var labels = await _dataStorageService.GetDataFromStorage();

            var searchedLabels = new List<Label>();
            foreach(var label in labels)
            {
                if (label.Name.ToUpper().Contains(formattedSearchQuery) || 
                   (!string.IsNullOrWhiteSpace(label.Description) && label.Description.ToUpper().Contains(formattedSearchQuery)))
                    searchedLabels.Add(label);
                else if (label.Beverages.Any() is false)
                    continue;
                else if(label.Beverages
                    .Any(b => 
                        b.Name.ToUpper().Contains(formattedSearchQuery) || 
                        (!string.IsNullOrWhiteSpace(b.Description) && b.Description.ToUpper().Contains(formattedSearchQuery))))
                    searchedLabels.Add(label);
            }

            Labels.Clear();

            foreach(var label in searchedLabels)
                Labels.Add(label);
        }

        [RelayCommand]
        async Task ResetSearch()
        {
            await GetData();
        }

        [RelayCommand]
        async Task GoToAddLabelPage()
        {
            await Shell.Current.GoToAsync(nameof(AddLabelPage), true);
        }

        [RelayCommand]
        async Task GoToBeverages(Guid labelId)
        {
            await Shell.Current.GoToAsync(nameof(BeveragesPage), true,
                new Dictionary<string, object>
                {
                    {"LabelId", labelId }
                });
        }
    }
}
