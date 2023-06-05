using LoLApp.ViewModel;

namespace LoLApp.UI.Pages;

public partial class AddChampionPage : ContentPage
{
    private ApplicationVM AppVM { get; set; }
    
    public AddChampionPage(ApplicationVM appVM)
    {
        AppVM = appVM;
        InitializeComponent();
        BindingContext = AppVM;
    }
    
    protected override bool OnBackButtonPressed()
    {
        AppVM.CancelCommand.Execute(null);
        return true;
    }
}

