using ViewModel.Enums;

namespace LoLApp.UI.Views;

public partial class ClassEditView : ContentView
{
    public ChampionClassVM SelectedItem
    {
        get => (ChampionClassVM)GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }
    public static readonly BindableProperty SelectedItemProperty =
        BindableProperty.Create(nameof(SelectedItem), typeof(ChampionClassVM), typeof(ClassEditView), default, BindingMode.TwoWay);
    
    public ClassEditView()
    {
        InitializeComponent();
    }
}

