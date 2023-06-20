using LoLApp.ViewModel;

namespace LoLApp.UI.Pages;

public partial class AddSkinPage : ContentPage
{
    private ApplicationVM AppVM { get; }
    public AddSkinPage(ApplicationVM appVM)
    {
        AppVM = appVM;
        InitializeComponent();
        BindingContext = AppVM;
    }
    
    protected override bool OnBackButtonPressed() => true;
}

