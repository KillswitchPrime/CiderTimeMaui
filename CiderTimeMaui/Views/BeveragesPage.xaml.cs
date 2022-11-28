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

    void OnPickerSelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = sender as Picker;
        int selectedIndex = picker.SelectedIndex;

        var viewModel = BindingContext as BeveragesViewModel;

        if (selectedIndex is not -1)
        {
            viewModel.SortBeveragesList(selectedIndex);
        }
    }
}