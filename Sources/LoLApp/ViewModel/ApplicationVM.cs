#nullable enable

using System.Windows.Input;
using LoLApp.UI.Pages;
using MVVMToolkit;
using ViewModel;
using ViewModel.ChampionVMs;

namespace LoLApp.ViewModel;

public class ApplicationVM: ObservableObject
{
    private readonly INavigation navigation;
    public ChampionMgrVM MgrVm { get; }
    
    public ChampionVM? SelectedChampion
    {
        get => selectedChampion;
        set => SetProperty(ref selectedChampion, value);
    }
    private ChampionVM? selectedChampion;
    
    // maybe useless
    public EditableChampionVM? EditableChampion
    {
        get => editableChampion;
        set => SetProperty(ref editableChampion, value);
    }
    private EditableChampionVM? editableChampion;
    
    public ICommand DetailPageCommand { get; }
    
    public ICommand AddChampionPageCommand { get; }
    
    public ICommand EditChampionPageCommand { get; }
    
    public ICommand ValidateChampionCommand { get; }
    
    public ICommand AddCharacteristicCommand { get; }
    
    public ICommand CancelCommand { get; }
    
    public ICommand BackCommand { get; }
    
    
    public ApplicationVM(ChampionMgrVM mgrVm)
    {
        navigation = Application.Current!.MainPage!.Navigation;
        MgrVm = mgrVm;
        
        DetailPageCommand = new Command<ChampionVM>(OnDetailPageCommand);
        
        AddChampionPageCommand = new Command(OnAddChampionPageCommand);
        EditChampionPageCommand = new Command(OnEditChampionPageCommand);
        
        AddCharacteristicCommand = new Command<KeyValuePair<string, int>>(OnAddCharacteristicCommand);
        ValidateChampionCommand = new Command(OnValidateChampionCommand);
        
        CancelCommand = new Command(OnCancelCommand);
        BackCommand = new Command(OnBackCommand);
    }
    
    private async void OnDetailPageCommand(ChampionVM championVm)
    {
        SelectedChampion = championVm;
        await navigation.PushAsync(new ChampionDetailPage(this));
    }
    
    private async void OnAddChampionPageCommand()
    {
        EditableChampion = new EditableChampionVM();
        await navigation.PushModalAsync(new AddChampionPage(this));
    }
    
    private async void OnEditChampionPageCommand()
    {
        EditableChampion = new EditableChampionVM(SelectedChampion!);
        await navigation.PushModalAsync(new AddChampionPage(this));
    }
    
    private void OnAddCharacteristicCommand(KeyValuePair<string, int> kvp) 
        => EditableChampion!.AddCharacteristic(kvp.Key, kvp.Value);

    private async void OnValidateChampionCommand()
    {
        if (SelectedChampion is null)
        {
            _ = await MgrVm.AddChampion(EditableChampion!.ToChampionVM());
            await navigation.PopAsync();
        }
        else
        {
            var updatedChamp = EditableChampion!.ToChampionVM();
            var updated = await MgrVm.UpdateChampion(selectedChampion!, updatedChamp);
            
            if (updated)
            {
                SelectedChampion = updatedChamp;
            }
            
            await navigation.PopAsync();
        }
    }
    
    private async void OnCancelCommand()
    {
        EditableChampion = null;
        await navigation.PopModalAsync();
    }
    
    private async void OnBackCommand()
    {
        SelectedChampion = null;
        await navigation.PopAsync();
    }
}
