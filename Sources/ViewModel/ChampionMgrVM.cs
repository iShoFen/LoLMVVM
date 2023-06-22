using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Model;
using MVVMToolkit;
using ViewModel.ChampionVMs;
using ViewModel.SkinVms;

namespace ViewModel;

[SuppressMessage("Interoperability", "CA1416")]
public partial class ChampionMgrVM : ObservableObject<IDataManager>
{
    public ReadOnlyObservableCollection<ChampionVM> Champions { get; }
    private readonly ObservableCollection<ChampionVM> champions = new();

    public int Page => GetPageCount().Result;
    
    // We cannot use ObservableProperty and NotifyCanExecuteChangedFor cause Index property return index + 1
    public int Index
    {
        get => index + 1;
        set => SetProperty(ref index, value - 1);
    }
    private int index;
    
    [ObservableProperty]
    private int count = 5;

    [ObservableProperty]
    private bool isOrderedDescending;
    
    [ObservableProperty]
    private ChampionVM? selectedChampion;
    
    [ObservableProperty]
    private SkinVM? selectedSkin;
    
    public ChampionMgrVM(IDataManager model) : base(model) 
        => Champions = new ReadOnlyObservableCollection<ChampionVM>(champions);

    private async Task Update()
    {
        if (Index > Page) Index = Page;
        await LoadChampions();
        
        // need to manually notify the command to update the CanExecute (because we cannot use ObservableProperty)
        PreviousPageCommand.NotifyCanExecuteChanged();
        NextPageCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand]
    private async Task LoadChampions()
    {
        champions.Clear();
        var champs = await Model.ChampionsMgr.GetItems(index, Count, nameof(Champion.Name), IsOrderedDescending);
        foreach (var champion in champs.Where(champion => champion != null))
        {
            champions.Add(new ChampionVM(champion!));
        }
    }

    [RelayCommand]
    private async Task SortChampion()
    {
        IsOrderedDescending = !IsOrderedDescending;
        await LoadChampions();
    }

    [RelayCommand(CanExecute = nameof(CanPreviousPage))]
    private async Task PreviousPage()
    {
        Index -= 1;
        await Update();
    }
    
    private bool CanPreviousPage() => index > 0;

    [RelayCommand(CanExecute = nameof(CanNextPage))]
    private async Task NextPage()
    {
        Index += 1;
        await Update();
    }
    
    private bool CanNextPage() => index < Page - 1;
    
    private async Task<int> GetPageCount()
    {
        var nbPage = (int) MathF.Ceiling((float) await Model.ChampionsMgr.GetNbItems() / Count);
        
        return nbPage > 0 ? nbPage : 1;
    }

    public async Task<bool> AddChampion(ChampionVM champion)
    {
        var result = await Model.ChampionsMgr.AddItem(champion.Model) != null;
        if (!result) return result;
        
        await Update();
        OnPropertyChanged(nameof(Page));

        return result;
    }
    
    public async Task<bool> UpdateChampion(ChampionVM newChampion)
    {
        var result = await Model.ChampionsMgr.UpdateItem(SelectedChampion!.Model, newChampion.Model) != null;
        if (result) await Update();

        return result;
    }

    public async Task<bool> RemoveChampion(ChampionVM champion)
    {
        if (!await Model.ChampionsMgr.DeleteItem(champion.Model)) return false;

        OnPropertyChanged(nameof(Page));
        await Update();

        return true;
    }
    
    public async Task<bool> AddSkin(SkinVM skin) => await Model.SkinsMgr.AddItem(skin.Model) != null;
    
    public async Task<bool> UpdateSkin(SkinVM skin) 
        => await Model.SkinsMgr.UpdateItem(SelectedSkin!.Model, skin.Model) != null;

    public async Task<bool> RemoveSkin(SkinVM skin)
    {
        if (SelectedChampion!.RemoveSkin(skin))
        {
            return await Model.SkinsMgr.DeleteItem(skin.Model);
        }
        
        return false;
    }
}
