using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Model;
using MVVMToolkit;

namespace ViewModel;

[SuppressMessage("Interoperability", "CA1416")]
public class ChampionMgrVM: ObservableObject<IDataManager>
{
    public ReadOnlyObservableCollection<ChampionVM> Champions { get; private set; }
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
    
    public ICommand LoadChampionsCommand { get; }
    
    public ICommand PreviousPageCommand { get; }

    public ICommand NextPageCommand { get; }

    public ChampionMgrVM(IDataManager model) : base(model)
    {
        Champions = new ReadOnlyObservableCollection<ChampionVM>(champions);
        PropertyChanged += LocalOnPropertyChanged;

        // Only done to avoid the warning for (using async on void method is not recommended)
        async void Execute() => await LoadChampions();
        LoadChampionsCommand = new Command(Execute, () => true);
        
        PreviousPageCommand = new Command(() => Index = index - 1, () => index > 0);
        NextPageCommand = new Command(() => Index = index + 1, () => index < Page - 1);
    }

    private async void LocalOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is not (nameof(Index) or nameof(Count))) return;
        
        await LoadChampions();
        (PreviousPageCommand as Command)?.ChangeCanExecute();
        (NextPageCommand as Command)?.ChangeCanExecute();
    }

    private async Task LoadChampions()
    {
        champions.Clear();
        var champs = await Model.ChampionsMgr.GetItems(index, count);
        // select only the champions that are not null
        foreach (var champion in champs.Where(champion => champion != null))
        {
            champions.Add(new ChampionVM(champion!));
        }
    }
}
