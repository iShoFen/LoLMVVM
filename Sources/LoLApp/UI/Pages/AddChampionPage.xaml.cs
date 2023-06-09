using LoLApp.ViewModel;

namespace LoLApp.UI.Pages;

public partial class AddChampionPage : ContentPage
{
    public ApplicationVM AppVM => ((App) Application.Current)!.AppVM;
    
    public AddChampionPage()
    {
        InitializeComponent();
        BindingContext = AppVM;
    }
    
    protected override bool OnBackButtonPressed() => true;
}

