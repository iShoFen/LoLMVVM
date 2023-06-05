using LoLApp.ViewModel;
using ViewModel;

namespace LoLApp.UI.Pages;

public partial class ChampionDetailPage : ContentPage
{
    private ApplicationVM AppVM { get; set; }
    
    public ChampionDetailPage(ApplicationVM appVM)
    {
        AppVM = appVM;
        InitializeComponent();
        BindingContext = AppVM;
    }

    private void Button_OnClicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}

