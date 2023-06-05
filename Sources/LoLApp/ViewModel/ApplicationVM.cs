using System.Windows.Input;
using LoLApp.UI.Pages;
using ViewModel;

namespace LoLApp.ViewModel;

public class ApplicationVM
{
    private readonly INavigation navigation;
    public ChampionMgrVM MgrVm { get; }
    
    public ICommand DetailPageCommand { get; }
    
    public ApplicationVM(ChampionMgrVM mgrVm)
    {
        navigation = Application.Current!.MainPage!.Navigation;
        MgrVm = mgrVm;
        
        DetailPageCommand = new Command<ChampionVM>(OnDetailPageCommand);
    }
    
    private void OnDetailPageCommand(ChampionVM championVm)
    {
        MgrVm.SelectedChampion = championVm;
        navigation.PushAsync(new ChampionDetailPage(this));
    }
}
