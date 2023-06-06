namespace LoLApp.UI.Views;

public partial class ClassEditView : ContentView
{
    public Enum SelectedItem
    {
        get => (Enum)GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }
    public static readonly BindableProperty SelectedItemProperty =
        BindableProperty.Create(nameof(SelectedItem), typeof(Enum), typeof(ClassEditView), default, BindingMode.TwoWay);
    
    public ClassEditView()
    {
        InitializeComponent();
    }
}

