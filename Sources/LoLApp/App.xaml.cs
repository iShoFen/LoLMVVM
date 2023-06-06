using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Platform;

namespace LoLApp;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		InitStyleHandlers();

		MainPage = new AppShell();
	}

	private static void InitStyleHandlers()
	{
		Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("LoLCustomization", (handler, _) =>
			{
				#if __ANDROID__
					var primaryColor = (Color) Resources.MergedDictionaries.First()["Primary"];
					handler.PlatformView.TextCursorDrawable!.SetTint(primaryColor.ToPlatform());
					handler.PlatformView.SetBackgroundColor(Colors.Transparent.ToPlatform());
				#endif
			}
		);

		Microsoft.Maui.Handlers.EditorHandler.Mapper.AppendToMapping("LoLCustomization", (handler, _) =>
			{
				#if __ANDROID__
					handler.PlatformView.SetBackgroundColor(Colors.Transparent.ToPlatform());
				#endif
			}
		);
	}
}
	