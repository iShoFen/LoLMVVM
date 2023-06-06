using System.Windows.Input;
using MVVMToolkit;

namespace LoLApp.UI.Views;

public partial class CharacteristicsEditView : ContentView
{
    public IDictionary<string, int> Characteristics
    {
        get => (IDictionary<string, int>) GetValue(CharacteristicsProperty);
        set => SetValue(CharacteristicsProperty, value);
    }
    public static readonly BindableProperty CharacteristicsProperty =
        BindableProperty.Create(nameof(Characteristics), typeof(IDictionary<string, int>), typeof(CharacteristicsEditView));
    
    public ICommand AddCommand
    {
        get => (ICommand) GetValue(AddCommandProperty);
        set => SetValue(AddCommandProperty, value);
    }
    public static readonly BindableProperty AddCommandProperty =
        BindableProperty.Create(nameof(AddCommand), typeof(ICommand), typeof(CharacteristicsEditView));

    public string Key
    {
        get => (string) GetValue(KeyProperty);
        set => SetValue(KeyProperty, value);
    }
    private static readonly BindableProperty KeyProperty =
        BindableProperty.Create(nameof(Key), typeof(string), typeof(CharacteristicsEditView));

    public int Value
    {
        get => (int) GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }
    private static readonly BindableProperty ValueProperty =
        BindableProperty.Create(nameof(Value), typeof(int), typeof(CharacteristicsEditView));

    public CharacteristicsEditView()
    {
        InitializeComponent();
    }
}

