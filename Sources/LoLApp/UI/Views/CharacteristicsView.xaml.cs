namespace LoLApp.UI.Views;

public partial class CharacteristicsView : ContentView
{
    public IDictionary<string, int> Characteristics
    {
        get => (IDictionary<string, int>)GetValue(CharacteristicsProperty);
        set => SetValue(CharacteristicsProperty, value);
    }
    public static readonly BindableProperty CharacteristicsProperty =
        BindableProperty.Create(nameof(Characteristics), typeof(IDictionary<string, int>), typeof(CharacteristicsView));
    
    public CharacteristicsView()
    {
        InitializeComponent();
    }
}

