
using LoLApp.ViewModel;

namespace LoLApp.UI.Pages;

public partial class SkinDetailPage : ContentPage
{
    private ApplicationVM AppVM { get; }
    public SkinDetailPage(ApplicationVM appVM)
    {
        AppVM = appVM;
        InitializeComponent();
        BindingContext = AppVM;
    }
}

