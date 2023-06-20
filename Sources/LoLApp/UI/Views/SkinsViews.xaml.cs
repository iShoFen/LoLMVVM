using LoLApp.ViewModel;

namespace LoLApp.UI.Views;

public partial class SkinsViews : ContentView
{
    public ApplicationVM AppVM
    {
        get => (ApplicationVM)GetValue(AppVMProperty);
        set => SetValue(AppVMProperty, value);
    }
    public static readonly BindableProperty AppVMProperty =
        BindableProperty.Create(nameof(AppVM), typeof(ApplicationVM), typeof(SkinsViews));
    
    public SkinsViews()
    {
        InitializeComponent();
    }
}

