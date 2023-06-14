using System.Windows.Input;
using LoLApp.ViewModel;
using ViewModel.ChampionVMs;

namespace LoLApp.UI.Views;

public partial class ChampionEditView : ContentView
{
    public EditApplicationChampionVM EditAppChampionVM => ((App) Application.Current!).EditAppChampionVM;
    public ChampionEditView()
    {
        InitializeComponent();
        BindingContext = EditAppChampionVM;
    }
}

