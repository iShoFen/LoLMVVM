using LoLApp.ViewModel;

namespace LoLApp.UI.Pages;

public partial class ChampionDetailPage : ContentPage
{
    public ApplicationVM AppVM => ((App) Application.Current)!.AppVM;
    
    public ChampionDetailPage()
    {
        InitializeComponent();
        BindingContext = AppVM;
    }
    
    protected override bool OnBackButtonPressed()
    {
        AppVM.BackCommand.Execute(null);
        return true;
    }
}

