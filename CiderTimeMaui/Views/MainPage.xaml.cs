using CiderTimeMaui.ViewModels;
using System.Linq;
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

	void OnPickerSelectedIndexChanged(object sender, EventArgs e)
	{
		var picker = sender as Picker;
		int selectedIndex = picker.SelectedIndex;

        var viewModel = BindingContext as LabelsViewModel;

        if (selectedIndex is not -1 ) 
			viewModel.SortLabelList(selectedIndex);
	}
}

