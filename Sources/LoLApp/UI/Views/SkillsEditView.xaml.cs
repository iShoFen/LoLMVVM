using LoLApp.ViewModel;

namespace LoLApp.UI.Views;

public partial class SkillsEditView : ContentView
{
    public  EditApplicationChampionVM EditAppVM
    {
        get => (EditApplicationChampionVM)GetValue(EditAppVMProperty);
        set => SetValue(EditAppVMProperty, value);
    }
    public static readonly BindableProperty EditAppVMProperty =
        BindableProperty.Create(nameof(EditAppVM), typeof(EditApplicationChampionVM), typeof(SkillsEditView));
    
    public SkillsEditView()
    {
        InitializeComponent();
    }
}

