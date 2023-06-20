using LoLApp.ViewModel;

namespace LoLApp.UI.Pages;

public partial class ChampionDetailPage : ContentPage
{
    public ApplicationVM AppVM { get; }
    
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

