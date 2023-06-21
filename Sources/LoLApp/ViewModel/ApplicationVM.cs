#nullable enable

using System.Windows.Input;
using LoLApp.UI.Pages;
using MVVMToolkit;
using ViewModel;
using ViewModel.ChampionVMs;
using ViewModel.SkinVms;

namespace LoLApp.ViewModel;

public class ApplicationVM: ObservableObject
{
    private static Shell Shell => Shell.Current;
    public ChampionMgrVM MgrVM { get; }
    public EditApplicationChampionVM EditAppChampionVM { get; }
    public EditApplicationSkinVM EditAppSkinVM { get; }
    
    public ICommand DetailPageCommand { get; }
    public ICommand AddChampionPageCommand { get; }
    public ICommand EditChampionPageCommand { get; }
    public ICommand DeleteChampionCommand { get; }
    public ICommand CancelCommand { get; }
    public ICommand BackCommand { get; }
    public ICommand DetailSkinPageCommand { get; }
    public ICommand AddSkinPageCommand { get; }
    public ICommand EditSkinPageCommand { get; }
    public ICommand DeleteSkinCommand { get; }
    public ICommand CancelSkinCommand { get; }
    
    public ApplicationVM(ChampionMgrVM mgrVM, EditApplicationChampionVM editAppChampionVM, EditApplicationSkinVM editAppSkinVM)
    {
        MgrVM = mgrVM;
        EditAppChampionVM = editAppChampionVM;
        EditAppSkinVM = editAppSkinVM;
        
        DetailPageCommand = new Command<ChampionVM>(OnDetailPageCommand);
        
        AddChampionPageCommand = new Command(OnAddChampionPageCommand);
        EditChampionPageCommand = new Command<ChampionVM?>(OnEditChampionPageCommand);
        DeleteChampionCommand = new Command<ChampionVM>(OnDeleteChampionCommand);
        
        CancelCommand = new Command(OnCancelCommand);
        BackCommand = new Command(OnBackCommand);
        
        DetailSkinPageCommand = new Command<SkinVM>(OnDetailSkinPageCommand);
        AddSkinPageCommand = new Command(OnAddSkinCommand);
        EditSkinPageCommand = new Command<SkinVM?>(OnEditSkinPageCommand);
        DeleteSkinCommand = new Command<SkinVM>(OnDeleteSkinCommand);
        CancelSkinCommand = new Command(OnCancelSkinCommand);
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
        if (Shell.Navigation.NavigationStack.Count == 1) MgrVM.SelectedChampion = null;
        await Shell.GoToAsync("..", true);
    }
    
    private async void OnDetailSkinPageCommand(SkinVM skinVM)
    {
        MgrVM.SelectedSkin = skinVM;
        await Shell.GoToAsync(nameof(SkinDetailPage), true);
    }
    
    private async void OnAddSkinCommand()
    {
        EditAppSkinVM.EditableSkin = new EditableSkinVM();
        await Shell.GoToAsync(nameof(AddSkinPage), true);
    }
    
    private async void OnEditSkinPageCommand(SkinVM? skinVM)
    {
        if (skinVM is not null) MgrVM.SelectedSkin = skinVM;
        
        EditAppSkinVM.EditableSkin = new EditableSkinVM(MgrVM.SelectedSkin!);
        await Shell.GoToAsync(nameof(AddSkinPage), true);
    }
    
    private async void OnDeleteSkinCommand(SkinVM skin) => await MgrVM.RemoveSkin(skin);
    
    private async void OnCancelSkinCommand()
    {
        EditAppSkinVM.EditableSkin = null;
        if (Shell.Navigation.NavigationStack.Count == 2) MgrVM.SelectedSkin = null;
        await Shell.GoToAsync("..", true);
    }
}
