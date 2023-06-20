#nullable enable

using System.Windows.Input;
using LoLApp.UI.Pages;
using MVVMToolkit;
using ViewModel;
using ViewModel.ChampionVMs;

namespace LoLApp.ViewModel;

public class ApplicationVM: ObservableObject
{
    private static Shell Shell => Shell.Current;
    public ChampionMgrVM MgrVM { get; }
    public EditApplicationChampionVM EditAppChampionVM { get; }
    
    public ICommand DetailPageCommand { get; }
    public ICommand AddChampionPageCommand { get; }
    public ICommand EditChampionPageCommand { get; }
    public ICommand DeleteChampionCommand { get; }
    public ICommand CancelCommand { get; }
    public ICommand BackCommand { get; }
    
    public ApplicationVM(ChampionMgrVM mgrVM, EditApplicationChampionVM editAppChampionVM)
    {
        MgrVM = mgrVM;
        EditAppChampionVM = editAppChampionVM;
        
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
        await Shell.GoToAsync(nameof(ChampionDetailPage), true);
    }
    
    private async void OnAddChampionPageCommand()
    {
        EditAppChampionVM.EditableChampion = new EditableChampionVM();
        await Shell.GoToAsync(nameof(AddChampionPage), true);
    }
    
    private async void OnEditChampionPageCommand(ChampionVM? championVm)
    {
        if (championVm is not null) MgrVM.SelectedChampion = championVm;
        
        EditAppChampionVM.EditableChampion = new EditableChampionVM(MgrVM.SelectedChampion!);
        await Shell.GoToAsync(nameof(AddChampionPage), true);
    }
    
    private async void OnDeleteChampionCommand(ChampionVM championVm) 
        => await MgrVM.RemoveChampion(championVm);
    
    
    private async void OnCancelCommand()
    {
        EditAppChampionVM.EditableChampion = null;
        if (Shell.Navigation.NavigationStack.Count == 1) MgrVM.SelectedChampion = null;
        await Shell.GoToAsync("..", true);
    }
    
    private async void OnBackCommand()
    {
        MgrVM.SelectedChampion = null;
        await Shell.GoToAsync("..", true);
    }
}
