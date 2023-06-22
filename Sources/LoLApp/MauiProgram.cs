using CommunityToolkit.Maui;
using LoLApp.UI.Pages;
using LoLApp.ViewModel;
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
				}
			)
			.Services
			.AddSingleton<IDataManager, StubData>()
			.AddSingleton<ChampionMgrVM>()
			.AddSingleton<EditApplicationChampionVM>()
			.AddSingleton<EditApplicationSkinVM>()
			.AddSingleton<ApplicationVM>()
			.AddSingleton<HomePage>()
			.AddSingleton<ChampionsPage>()
			.AddTransient<ChampionDetailPage>()
			.AddTransient<SkinDetailPage>()
			.AddTransient<AddSkinPage>()
			.AddTransient<AddChampionPage>()
			.AddTransient<AddSkillPage>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
