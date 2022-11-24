using CiderTimeMaui.ViewModels;

namespace CiderTimeMaui.Views;

public partial class EditLabelPage : ContentPage
{
	public EditLabelPage(EditLabelViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    protected override async void OnAppearing()
    {
		var viewModel = BindingContext as EditLabelViewModel;
		await viewModel.GetData();

        base.OnAppearing();
    }
}