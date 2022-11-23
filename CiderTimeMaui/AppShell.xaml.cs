using CiderTimeMaui.Views;

namespace CiderTimeMaui;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(AddLabelPage), typeof(AddLabelPage));
        Routing.RegisterRoute(nameof(BeveragesPage), typeof(BeveragesPage));
        Routing.RegisterRoute(nameof(AddBeveragePage), typeof(AddBeveragePage));
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
    }
}
