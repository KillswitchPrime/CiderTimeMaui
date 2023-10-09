using CiderTimeMaui.Services;
using CiderTimeMaui.Services.Interfaces;
using CiderTimeMaui.ViewModels;
using CiderTimeMaui.Views;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace CiderTimeMaui;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
            builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		// Services
        builder.Services.AddSingleton<IDataStorageService, DataStorageService>();
		builder.Services.AddSingleton<IMediaService, MediaService>();
		builder.Services.AddScoped<IPermissionsService, PermissionsService>();

		// Views and ViewModels
        builder.Services.AddSingleton<MainPage, LabelsViewModel>();
		builder.Services.AddTransient<AddLabelPage, AddLabelViewModel>();
		builder.Services.AddTransient<BeveragesPage, BeveragesViewModel>();
		builder.Services.AddTransient<AddBeveragePage, AddBeverageViewModel>();
        builder.Services.AddTransient<EditBeveragePage, EditBeverageViewModel>();
        builder.Services.AddTransient<EditLabelPage, EditLabelViewModel>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
