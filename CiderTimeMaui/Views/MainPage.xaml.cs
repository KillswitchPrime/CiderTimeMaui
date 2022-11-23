using CiderTimeMaui.ViewModels;
using System.Runtime.CompilerServices;

namespace CiderTimeMaui;

public partial class MainPage : ContentPage
{
	public MainPage(LabelsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    protected override async void OnAppearing()
    {
		var viewModel = BindingContext as LabelsViewModel;
		await viewModel.GetData();
        base.OnAppearing();
    }
}

