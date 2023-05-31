using LoLApp.UI.Pages;
using ViewModel;

namespace LoLApp.UI.Pages;

public partial class ChampionsPage : ContentPage
{
    
    private ChampionMgrVM MgrVm { get; set; }
    
    public ChampionsPage(ChampionMgrVM mgrVm)
    {
        MgrVm = mgrVm;
        InitializeComponent();
        BindingContext = MgrVm;
    }
    
    private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
    {
        MgrVm.SelectedChampion = e.Item as ChampionVM;
        Navigation.PushAsync(new ChampionDetailPage(MgrVm));
    }
}