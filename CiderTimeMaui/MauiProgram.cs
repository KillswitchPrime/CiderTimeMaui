using CiderTimeMaui.Services;
using CiderTimeMaui.Services.Interfaces;
using CiderTimeMaui.ViewModels;
using CiderTimeMaui.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CiderTimeMaui;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<IDataStorageService, DataStorageService>();
		builder.Services.AddSingleton<LabelsViewModel>();

		builder.Services.AddTransient<AddLabelViewModel>();
		builder.Services.AddTransient<AddLabelPage>();

		builder.Services.AddTransient<BeveragesPage>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
