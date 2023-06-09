#nullable enable

using System.Windows.Input;
using LoLApp.UI.Pages;
using MVVMToolkit;
using ViewModel;
using ViewModel.ChampionVMs;

namespace LoLApp.ViewModel;

public class ApplicationVM: ObservableObject
{
    private  INavigation Navigation => Application.Current!.MainPage!.Navigation;
    public ChampionMgrVM MgrVM => ((App) Application.Current!).MgrVM;
    public EditApplicationChampionVM EditAppChampionVM => ((App) Application.Current!).EditAppChampionVM;
    
    public ICommand DetailPageCommand { get; }
    public ICommand AddChampionPageCommand { get; }
    public ICommand EditChampionPageCommand { get; }
    public ICommand DeleteChampionCommand { get; }
    public ICommand CancelCommand { get; }
    public ICommand BackCommand { get; }
    
    public ApplicationVM()
    {
        DetailPageCommand = new Command<ChampionVM>(OnDetailPageCommand);
        
        AddChampionPageCommand = new Command(OnAddChampionPageCommand);
        EditChampionPageCommand = new Command<ChampionVM?>(OnEditChampionPageCommand);
        DeleteChampionCommand = new Command<ChampionVM>(OnDeleteChampionCommand);
        
        CancelCommand = new Command(OnCancelCommand);
        BackCommand = new Command(OnBackCommand);
    }
    
    private async void OnDetailPageCommand(ChampionVM championVm)
    {
        MgrVM.SelectedChampion = championVm;
        await Navigation.PushAsync(new ChampionDetailPage());
    }
    
    private async void OnAddChampionPageCommand()
    {
        EditAppChampionVM.EditableChampion = new EditableChampionVM();
        await Navigation.PushModalAsync(new AddChampionPage());
    }
    
    private async void OnEditChampionPageCommand(ChampionVM? championVm)
    {
        if (championVm is not null) MgrVM.SelectedChampion = championVm;
        
        EditAppChampionVM.EditableChampion = new EditableChampionVM(MgrVM.SelectedChampion!);
        await Navigation.PushModalAsync(new AddChampionPage());
    }
    
    private async void OnDeleteChampionCommand(ChampionVM championVm) 
        => await MgrVM.RemoveChampion(championVm);
    
    
    private async void OnCancelCommand()
    {
        EditAppChampionVM.EditableChampion = null;
        if (Navigation.NavigationStack.Count == 1) MgrVM.SelectedChampion = null;
        await Navigation.PopModalAsync();
    }
    
    private async void OnBackCommand()
    {
        MgrVM.SelectedChampion = null;
        await Navigation.PopAsync();
    }
}
