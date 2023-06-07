using LoLApp.ViewModel;
using ViewModel;

namespace LoLApp.UI.Pages;

public partial class ChampionsPage : ContentPage
{
    public ApplicationVM AppVM { get; set; }
    
    public ChampionsPage(ApplicationVM appVM)
    {
        AppVM = appVM;
        InitializeComponent();
        BindingContext = AppVM;
    }
    
}