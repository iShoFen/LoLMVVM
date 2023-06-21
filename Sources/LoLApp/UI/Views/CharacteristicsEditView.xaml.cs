using System.Windows.Input;
using LoLApp.ViewModel;
using MVVMToolkit;

namespace LoLApp.UI.Views;

public partial class CharacteristicsEditView : ContentView
{
    public EditApplicationChampionVM EditAppChampionVM
    {
        get => (EditApplicationChampionVM) GetValue(EditAppChampionVMProperty);
        set => SetValue(EditAppChampionVMProperty, value);
    }
    public static readonly BindableProperty EditAppChampionVMProperty =
        BindableProperty.Create(nameof(EditAppChampionVM), typeof(EditApplicationChampionVM), typeof(CharacteristicsEditView));
    
    public CharacteristicsEditView()
    {
        InitializeComponent();
    }
}

