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
