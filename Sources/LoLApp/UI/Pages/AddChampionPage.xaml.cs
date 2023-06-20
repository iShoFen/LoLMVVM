using LoLApp.ViewModel;

namespace LoLApp.UI.Pages;

public partial class AddChampionPage : ContentPage
{
    public ApplicationVM AppVM { get; }
    
    public AddChampionPage(ApplicationVM appVM)
    {
        AppVM = appVM;
        InitializeComponent();
        BindingContext = AppVM;
    }
    
    protected override bool OnBackButtonPressed() => true;
}

