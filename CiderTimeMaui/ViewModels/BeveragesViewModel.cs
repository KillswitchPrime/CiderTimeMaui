using CommunityToolkit.Mvvm.ComponentModel;
using Label = CiderTimeMaui.Models.Label;

namespace CiderTimeMaui.ViewModels
{
    [QueryProperty("Label", nameof(Label))]
    public partial class BeveragesViewModel : ObservableObject
    {
        [ObservableProperty]
        Label label;
    }
}
