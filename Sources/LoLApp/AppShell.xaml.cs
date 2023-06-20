using LoLApp.UI.Pages;

namespace LoLApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		
		Routing.RegisterRoute(nameof(ChampionDetailPage), typeof(ChampionDetailPage));
		Routing.RegisterRoute(nameof(AddChampionPage), typeof(AddChampionPage));
		Routing.RegisterRoute(nameof(SkinDetailPage), typeof(SkinDetailPage));
		Routing.RegisterRoute(nameof(AddSkinPage), typeof(AddSkinPage));
	}
}
