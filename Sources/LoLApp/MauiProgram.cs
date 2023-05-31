using CommunityToolkit.Maui;
using LoLApp.UI.Pages;
using Microsoft.Extensions.Logging;
using Model;
using StubLib;
using ViewModel;

namespace LoLApp;

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
			})
			.Services
			.AddSingleton<IDataManager, StubData>()
			.AddSingleton<ChampionMgrVM>()
			.AddScoped<ChampionsPage>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
