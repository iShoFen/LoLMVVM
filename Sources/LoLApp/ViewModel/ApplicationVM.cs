#nullable enable

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LoLApp.UI.Pages;
using ViewModel;
using ViewModel.ChampionVMs;
using ViewModel.SkinVms;

namespace LoLApp.ViewModel;

public partial class ApplicationVM: ObservableObject
{
    private static Shell Shell => Shell.Current;
    public ChampionMgrVM MgrVM { get; }
    public EditApplicationChampionVM EditAppChampionVM { get; }
    public EditApplicationSkinVM EditAppSkinVM { get; }
    
    public ApplicationVM(ChampionMgrVM mgrVM, EditApplicationChampionVM editAppChampionVM, EditApplicationSkinVM editAppSkinVM)
    {
        MgrVM = mgrVM;
        EditAppChampionVM = editAppChampionVM;
        EditAppSkinVM = editAppSkinVM;
    }

    [RelayCommand]
    private async Task DetailPage(ChampionVM championVm)
    {
        MgrVM.SelectedChampion = championVm;
        await Shell.GoToAsync(nameof(ChampionDetailPage), true);
    }
    
    [RelayCommand]
    private async Task AddChampionPage()
    {
        EditAppChampionVM.EditableChampion = new EditableChampionVM();
        await Shell.GoToAsync(nameof(AddChampionPage), true);
    }
    
    [RelayCommand]
    private async Task EditChampionPage(ChampionVM? championVm)
    {
        if (championVm is not null) MgrVM.SelectedChampion = championVm;
        
        EditAppChampionVM.EditableChampion = new EditableChampionVM(MgrVM.SelectedChampion!);
        await Shell.GoToAsync(nameof(AddChampionPage), true);
    }
    
    [RelayCommand]
    private async Task DeleteChampion(ChampionVM championVm) 
        => await MgrVM.RemoveChampion(championVm);
    
    
    [RelayCommand]
    private async Task Cancel()
    {
        EditAppChampionVM.EditableChampion = null;
        if (Shell.Navigation.NavigationStack.Count == 1) MgrVM.SelectedChampion = null;
        await Shell.GoToAsync("..", true);
    }
    
    [RelayCommand]
    private async Task Back()
    {
        if (Shell.Navigation.NavigationStack.Count == 1) MgrVM.SelectedChampion = null;
        await Shell.GoToAsync("..", true);
    }
    
    [RelayCommand]
    private async Task DetailSkinPage(SkinVM skinVM)
    {
        MgrVM.SelectedSkin = skinVM;
        await Shell.GoToAsync(nameof(SkinDetailPage), true);
    }
    
    [RelayCommand]
    private async Task AddSkinPage()
    {
        EditAppSkinVM.EditableSkin = new EditableSkinVM();
        await Shell.GoToAsync(nameof(AddSkinPage), true);
    }
    
    [RelayCommand]
    private async Task EditSkinPage(SkinVM? skinVM)
    {
        if (skinVM is not null) MgrVM.SelectedSkin = skinVM;
        
        EditAppSkinVM.EditableSkin = new EditableSkinVM(MgrVM.SelectedSkin!);
        await Shell.GoToAsync(nameof(AddSkinPage), true);
    }
    
    [RelayCommand]
    private async Task DeleteSkin(SkinVM skin) => await MgrVM.RemoveSkin(skin);

    [RelayCommand]
    private async Task CancelSkin()
    {
        EditAppSkinVM.EditableSkin = null;
        if (Shell.Navigation.NavigationStack.Count == 2) MgrVM.SelectedSkin = null;
        await Shell.GoToAsync("..", true);
    }
}
