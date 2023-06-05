using System.Windows.Input;
using LoLApp.UI.Pages;
using ViewModel;
using ViewModel.ChampionVMs;

namespace LoLApp.ViewModel;

public class ApplicationVM
{
    private readonly INavigation navigation;
    public ChampionMgrVM MgrVm { get; }
    
    public ICommand DetailPageCommand { get; }
    
    public ICommand AddChampionPageCommand { get; }
    
    public ICommand EditChampionPageCommand { get; }
    
    public ICommand ValidateChampionCommand { get; }
    
    public ICommand CancelCommand { get; }
    
    public ICommand BackCommand { get; }
    
    public ApplicationVM(ChampionMgrVM mgrVm)
    {
        navigation = Application.Current!.MainPage!.Navigation;
        MgrVm = mgrVm;
        
        DetailPageCommand = new Command<ChampionVM>(OnDetailPageCommand);
        AddChampionPageCommand = new Command(OnAddChampionPageCommand);
        EditChampionPageCommand = new Command(OnEditChampionPageCommand);
        ValidateChampionCommand = new Command(OnValidateChampionCommand);
        CancelCommand = new Command(OnCancelCommand);
        BackCommand = new Command(OnBackCommand);
    }
    
    private async void OnDetailPageCommand(ChampionVM championVm)
    {
        MgrVm.SelectedChampion = championVm;
        await navigation.PushAsync(new ChampionDetailPage(this));
    }
    
    private async void OnAddChampionPageCommand()
    {
        MgrVm.EditableChampion = new EditableChampionVM();
        await navigation.PushAsync(new AddChampionPage(this));
    }
    
    private async void OnEditChampionPageCommand()
    {
        MgrVm.EditableChampion = new EditableChampionVM(MgrVm.SelectedChampion!);
        await navigation.PushAsync(new AddChampionPage(this));
    }
    
    private async void OnValidateChampionCommand()
    {
        if (MgrVm.SelectedChampion is null)
        {
            _ = await MgrVm.AddChampion(MgrVm.EditableChampion!.ToChampionVM());
            await navigation.PopAsync();
        }
        else
        {
            var updatedChamp = MgrVm.EditableChampion!.ToChampionVM();
            var updated = await MgrVm.UpdateChampion(updatedChamp);
            
            if (updated)
            {
                MgrVm.SelectedChampion = updatedChamp;
            }
            
            await navigation.PopAsync();
        }
    }
    
    private async void OnCancelCommand()
    {
        MgrVm.EditableChampion = null;
        await navigation.PopAsync();
    }
    
    private async void OnBackCommand()
    {
        MgrVm.SelectedChampion = null;
        await navigation.PopAsync();
    }
}
