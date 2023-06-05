using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Model;
using MVVMToolkit;
using ViewModel.ChampionVMs;

namespace ViewModel;

[SuppressMessage("Interoperability", "CA1416")]
public class ChampionMgrVM: ObservableObject<IDataManager>
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
    
    public ChampionVM? SelectedChampion
    {
        get => selectedChampion;
        set => SetProperty(ref selectedChampion, value);
    }
    private ChampionVM? selectedChampion;
    
    public EditableChampionVM? EditableChampion
    {
        get => editableChampion;
        set => SetProperty(ref editableChampion, value);
    }
    private EditableChampionVM? editableChampion;

    public ICommand LoadChampionsCommand { get; }
    
    public ICommand SortChampionCommand { get; }
    
    public ICommand PreviousPageCommand { get; }

    public ICommand NextPageCommand { get; }
    
    public ChampionMgrVM(IDataManager model) : base(model)
    {
        Champions = new ReadOnlyObservableCollection<ChampionVM>(champions);
        PropertyChanged += LocalOnPropertyChanged;
        
        LoadChampionsCommand = new Command(ExecuteLoad);
        SortChampionCommand = new Command(SortChampion);
        
        PreviousPageCommand = new Command(() => Index = index - 1, () => index > 0);
        NextPageCommand = new Command(() => Index = index + 1, () => index < Page - 1);
    }
    
    private async void LocalOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is not 
            (nameof(Index) or 
             nameof(Count) or
             nameof(EditableChampion))) return;

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
    
    public async Task<bool> AddChampion(ChampionVM champion) 
        => await Model.ChampionsMgr.AddItem(champion.Model) != null;

    public async Task<bool> RemoveChampion(ChampionVM champion)
    {
        if (!await Model.ChampionsMgr.DeleteItem(champion.Model)) return false;
        
        champions.Remove(champion);
        return true;
    }

    public async Task<bool> UpdateChampion(ChampionVM champion) 
        => await Model.ChampionsMgr.UpdateItem(SelectedChampion!.Model, champion.Model) != null;
}
