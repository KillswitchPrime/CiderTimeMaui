using CiderTimeMaui.ViewModels;

namespace CiderTimeMaui.Views;

public partial class EditBeveragePage : ContentPage
{
	public EditBeveragePage(EditBeverageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    protected override async void OnAppearing()
    {
		var viewModel = BindingContext as EditBeverageViewModel;
		await viewModel.GetData();

        base.OnAppearing();
    }
}