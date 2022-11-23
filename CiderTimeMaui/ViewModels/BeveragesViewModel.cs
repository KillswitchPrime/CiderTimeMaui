using CiderTimeMaui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Label = CiderTimeMaui.Models.Label;

namespace CiderTimeMaui.ViewModels
{
    [QueryProperty("Label", nameof(Models.Label))]
    public partial class BeveragesViewModel : ObservableObject
    {
        [ObservableProperty]
        Label label;

        [RelayCommand]
        async Task GoToAddBeverage()
        {
            await Shell.Current.GoToAsync(nameof(AddBeveragePage), true, 
                new Dictionary<string, object>
                {
                    {"Id", Label.Id}
                });
        }
    }
}
