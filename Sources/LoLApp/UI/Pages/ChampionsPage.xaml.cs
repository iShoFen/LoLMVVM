using LoLApp.ViewModel;
using ViewModel;

namespace LoLApp.UI.Pages;

public partial class ChampionsPage : ContentPage
{
    public ApplicationVM AppVM => ((App) Application.Current)!.AppVM;
    
    public ChampionsPage()
    {
        InitializeComponent();
        BindingContext = AppVM;
    }
    
}