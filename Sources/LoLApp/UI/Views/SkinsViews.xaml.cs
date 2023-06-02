using ViewModel;

namespace LoLApp.UI.Views;

public partial class SkinsViews : ContentView
{
    public IEnumerable<SkinVM> Skins
    {
        get => (IEnumerable<SkinVM>)GetValue(SkinsProperty);
        set => SetValue(SkinsProperty, value);
    }
    public static readonly BindableProperty SkinsProperty = 
        BindableProperty.Create(nameof(Skins), typeof(IEnumerable<SkinVM>), typeof(SkinsViews));
    
    public SkinsViews()
    {
        InitializeComponent();
    }
}

