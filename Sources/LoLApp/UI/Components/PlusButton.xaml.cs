using System.Windows.Input;

namespace LoLApp.UI.Components;

public partial class PlusButton : ContentView
{
    public Color Color
    {
        get => (Color)GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }
    public static readonly BindableProperty ColorProperty = 
        BindableProperty.Create(nameof(Color), typeof(Color), typeof(PlusButton), Colors.Black);
    
    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }
    public static readonly BindableProperty CommandProperty = 
        BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(PlusButton));
    
    public object CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }
    public static readonly BindableProperty CommandParameterProperty = 
        BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(PlusButton));

    public PlusButton()
    {
        InitializeComponent();
    }
}

