﻿using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Model;
using MVVMToolkit;
using ViewModel.ChampionVMs;
using ViewModel.SkinVms;

namespace ViewModel;

[SuppressMessage("Interoperability", "CA1416")]
public class ChampionMgrVM : ObservableObject<IDataManager>
{
    public ReadOnlyObservableCollection<ChampionVM> Champions { get; }
    private readonly ObservableCollection<ChampionVM> champions = new();

    public int Page => GetPageCount().Result;

    public int Index
    {
        get => index + 1;
        set => SetProperty(ref index, value - 1);
    }

    private int index;

    public int Count
    {
        get => count;
        set => SetProperty(ref count, value);
    }

    private int count = 5;

    public bool IsOrderedDescending
    {
        get => isOrderedDescending;
        set => SetProperty(ref isOrderedDescending, value);
    }

    private bool isOrderedDescending;
    
    public ChampionVM? SelectedChampion
    {
        get => selectedChampion;
        set => SetProperty(ref selectedChampion, value);
    }
    private ChampionVM? selectedChampion;
    
    public SkinVM? SelectedSkin
    {
        get => selectedSkin;
        set => SetProperty(ref selectedSkin, value);
    }
    private SkinVM? selectedSkin;
    
    public ICommand LoadChampionsCommand { get; }

    public ICommand SortChampionCommand { get; }

    public ICommand PreviousPageCommand { get; }

    public ICommand NextPageCommand { get; }

    public ChampionMgrVM(IDataManager model) : base(model)
    {
        Champions = new ReadOnlyObservableCollection<ChampionVM>(champions);

        LoadChampionsCommand = new Command(ExecuteLoad);
        SortChampionCommand = new Command(SortChampion);

        PreviousPageCommand = new Command(PreviousPage, () => index > 0);
        NextPageCommand = new Command(NextPage, () => index < Page - 1);
    }

    private async Task Update()
    {
        if (Index > Page) Index = Page;
        await LoadChampions();
        (PreviousPageCommand as Command)?.ChangeCanExecute();
        (NextPageCommand as Command)?.ChangeCanExecute();
    }

    // Only done to avoid the warning for (using async on void method is not recommended)
    private async void ExecuteLoad() => await LoadChampions();

    private async Task LoadChampions()
    {
        champions.Clear();
        var champs = await Model.ChampionsMgr.GetItems(index, count, nameof(Champion.Name), IsOrderedDescending);
        foreach (var champion in champs.Where(champion => champion != null))
        {
            champions.Add(new ChampionVM(champion!));
        }
    }

    private async void SortChampion()
    {
        IsOrderedDescending = !IsOrderedDescending;
        await LoadChampions();
    }

    private async void PreviousPage()
    {
        Index -= 1;
        await Update();
    }

    private async void NextPage()
    {
        Index += 1;
        await Update();
    }
    
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
