using CiderTimeMaui.ViewModels;

namespace CiderTimeMaui.Views;

public partial class BeveragesPage : ContentPage
{
	public BeveragesPage(BeveragesViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    protected override async void OnAppearing()
    {
		var viewModel = BindingContext as BeveragesViewModel;
		await viewModel.GetBeverages();
        base.OnAppearing();
    }
}