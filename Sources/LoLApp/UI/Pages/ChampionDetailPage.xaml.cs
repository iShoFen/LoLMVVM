using LoLApp.ViewModel;

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
    
    protected override bool OnBackButtonPressed()
    {
        AppVM.BackCommand.Execute(null);
        return true;
    }
}

