using CiderTimeMaui.ViewModels;

namespace CiderTimeMaui.Views;

public partial class BeveragesPage : ContentPage
{
	public BeveragesPage(BeveragesViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}