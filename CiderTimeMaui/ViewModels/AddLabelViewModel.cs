using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Label = CiderTimeMaui.Models.Label;

namespace CiderTimeMaui.ViewModels
{
    public partial class AddLabelViewModel : ObservableObject
    {
        [ObservableProperty]
        Label label;

        [RelayCommand]
        async Task AddLabel()
        {
            await Shell.Current.GoToAsync(nameof(MainPage), true,
                new Dictionary<string, object>
                {
                    { "Label", Label }
                });
        }
    }
}
