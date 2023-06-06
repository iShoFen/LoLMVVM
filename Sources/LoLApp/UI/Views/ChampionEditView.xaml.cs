using System.Windows.Input;
using ViewModel.ChampionVMs;

namespace LoLApp.UI.Views;

public partial class ChampionEditView : ContentView
{
    public EditableChampionVM ChampionVm
    {
        get => (EditableChampionVM)GetValue(ChampionVmProperty);
        set => SetValue(ChampionVmProperty, value);
    }
    public static readonly BindableProperty ChampionVmProperty =
        BindableProperty.Create(nameof(ChampionVm), typeof(EditableChampionVM), typeof(ChampionEditView));
    
    public bool IsNew
    {
        get => (bool)GetValue(IsNewProperty);
        set => SetValue(IsNewProperty, value);
    }
    public static readonly BindableProperty IsNewProperty =
        BindableProperty.Create(nameof(IsNew), typeof(bool), typeof(ChampionEditView), true);
    
    public ICommand FilePickerCommand
    {
        get => (ICommand)GetValue(FilePickerCommandProperty);
        set => SetValue(FilePickerCommandProperty, value);
    }
    public static readonly BindableProperty FilePickerCommandProperty =
        BindableProperty.Create(nameof(FilePickerCommand), typeof(ICommand), typeof(ChampionEditView));
    
    public ICommand AddCharacteristicCommand
    {
        get => (ICommand)GetValue(AddCharacteristicCommandProperty);
        set => SetValue(AddCharacteristicCommandProperty, value);
    }
    public static readonly BindableProperty AddCharacteristicCommandProperty =
        BindableProperty.Create(nameof(AddCharacteristicCommand), typeof(ICommand), typeof(ChampionEditView));
    
    public ChampionEditView()
    {
        InitializeComponent();
    }
}

