using ViewModel;

namespace LoLApp.Views.Pages;

public partial class ChampionsPage : ContentPage
{
    
    private ChampionMgrVM MgrVm { get; set; }
    
    public ChampionsPage(ChampionMgrVM mgrVm)
    {
        MgrVm = mgrVm;
        InitializeComponent();
        BindingContext = MgrVm;
    }
}