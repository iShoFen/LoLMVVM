using LoLApp.ViewModel;

namespace LoLApp.UI.Views;

public partial class ChampionEditView : ContentView
{
    public EditApplicationChampionVM EditAppChampionVM
    {
        get => (EditApplicationChampionVM) GetValue(EditAppChampionVMProperty);
        set => SetValue(EditAppChampionVMProperty, value);
    }
    public static readonly BindableProperty EditAppChampionVMProperty =
        BindableProperty.Create(nameof(EditAppChampionVM), typeof(EditApplicationChampionVM), typeof(ChampionEditView));
    public ChampionEditView()
    {
        InitializeComponent();
    }
}

