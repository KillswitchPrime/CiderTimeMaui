using CiderTimeMaui.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiderTimeMaui.ViewModels
{
    [QueryProperty(nameof(Id), "BeverageId")]
    public partial class EditBeverageViewModel : ObservableObject
    {
        private readonly IDataStorageService _storageService;

        public EditBeverageViewModel(IDataStorageService storageService)
        {
            _storageService = storageService;
        }
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

        [RelayCommand]
        async Task FinishedEditing()
        {
            var labels = await _storageService.GetDataFromStorage();

            foreach (var label in labels.Where(x => x.Beverages.FirstOrDefault(b => b.Id == Id) is not null))
            {
                foreach(var beverage in label.Beverages)
                {
                    beverage.Name = Name;
                    beverage.Description = Description;
                    beverage.Rating = int.Parse(Rating);
                    beverage.Price = decimal.Parse(Price);
                }
            }

            await _storageService.WriteDataToStorage(labels);

            await Shell.Current.GoToAsync("..", true);
        }

        public async Task GetData()
        {
            var labels = await _storageService.GetDataFromStorage();

            var beverage = labels.FirstOrDefault(x => x.Beverages.FirstOrDefault(b => b.Id == Id) is not null)
                .Beverages
                .FirstOrDefault(b => b.Id == Id);

            Name = beverage.Name;
            Description = beverage.Description;
            Rating = beverage.Rating.ToString();
            Price = beverage.Price.ToString();
        }
    }
}
