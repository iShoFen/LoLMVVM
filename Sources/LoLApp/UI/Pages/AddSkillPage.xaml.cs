using LoLApp.ViewModel;

namespace LoLApp.UI.Pages;

public partial class AddSkillPage : ContentPage
{
    public EditApplicationChampionVM EditAppChampionVM { get; }
    
    public AddSkillPage(EditApplicationChampionVM editAppChampionVM)
    {
        EditAppChampionVM = editAppChampionVM;
        InitializeComponent();
        BindingContext = EditAppChampionVM;
    }
}

