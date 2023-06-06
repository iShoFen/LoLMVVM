using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Model;
using MVVMToolkit;
using ViewModel.ChampionVMs;

namespace ViewModel;

[SuppressMessage("Interoperability", "CA1416")]
public class ChampionMgrVM : ObservableObject<IDataManager>
{
    public ReadOnlyObservableCollection<ChampionVM> Champions { get; }
    private readonly ObservableCollection<ChampionVM> champions = new();

    public int Page => Model.ChampionsMgr.GetNbItems().Result / Count + 1;

    public int Index
    {
        get => index + 1;
        set => SetProperty(ref index, value);
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
        Index = index - 1;
        await Update();
    }

    private async void NextPage()
    {
        Index = index + 1;
        await Update();
    }

    public async Task<bool> AddChampion(ChampionVM champion)
    {
        var result = await Model.ChampionsMgr.AddItem(champion.Model) != null;
        if (result) await Update();

        return result;
    }
    
    public async Task<bool> UpdateChampion(ChampionVM oldChampion, ChampionVM newChampion)
    {
        var result = await Model.ChampionsMgr.UpdateItem(oldChampion.Model, newChampion.Model) != null;
        if (result) await Update();

        return result;
    }

    public async Task<bool> RemoveChampion(ChampionVM champion)
    {
        if (!await Model.ChampionsMgr.DeleteItem(champion.Model)) return false;
        champions.Remove(champion);

        return true;
    }
}
