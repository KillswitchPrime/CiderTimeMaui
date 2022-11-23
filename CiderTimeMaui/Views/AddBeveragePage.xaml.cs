using CiderTimeMaui.ViewModels;

namespace CiderTimeMaui.Views;

public partial class AddBeveragePage : ContentPage
{
	public AddBeveragePage(AddBeverageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}