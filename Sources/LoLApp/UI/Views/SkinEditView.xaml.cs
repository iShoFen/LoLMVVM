using LoLApp.ViewModel;

namespace LoLApp.UI.Views;

public partial class SkinEditView : ContentView
{
    public EditApplicationSkinVM EditAppSkinVM
    {
        get => (EditApplicationSkinVM) GetValue(EditAppSkinVMProperty);
        set => SetValue(EditAppSkinVMProperty, value);
    }
    public static readonly BindableProperty EditAppSkinVMProperty =
        BindableProperty.Create(nameof(EditAppSkinVM), typeof(EditApplicationSkinVM), typeof(SkinEditView));
    
    public SkinEditView()
    {
        InitializeComponent();
    }
}

