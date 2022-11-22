using CiderTimeMaui.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CiderTimeMaui.ViewModels
{
    public partial class BeveragesViewModel : ObservableObject
    {
        [ObservableProperty]
        Beverage beverage;
    }
}
