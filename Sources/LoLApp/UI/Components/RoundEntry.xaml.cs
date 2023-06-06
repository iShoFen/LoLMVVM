namespace LoLApp.UI.Components;

public partial class RoundEntry : ContentView
{
    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }
    public static readonly BindableProperty PlaceholderProperty = 
        BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(RoundEntry), string.Empty);
    
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
    public static readonly BindableProperty TextProperty = 
        BindableProperty.Create(nameof(Text), typeof(string), typeof(RoundEntry), string.Empty, BindingMode.TwoWay);
    
    public Keyboard Keyboard
    {
        get => (Keyboard)GetValue(KeyboardProperty);
        set => SetValue(KeyboardProperty, value);
    }
    public static readonly BindableProperty KeyboardProperty = 
        BindableProperty.Create(nameof(Keyboard), typeof(Keyboard), typeof(RoundEntry), Keyboard.Default);
    
    public RoundEntry()
    {
        InitializeComponent();
    }
}

