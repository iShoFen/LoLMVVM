using ViewModel;

namespace LoLApp.UI.Pages;

public partial class ChampionDetailPage : ContentPage
{
    private ChampionMgrVM MgrVm { get; set; }
    
    public ChampionDetailPage(ChampionMgrVM mgrVm)
    {
        MgrVm = mgrVm;
        InitializeComponent();
        BindingContext = MgrVm;
    }

    private void Button_OnClicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}

