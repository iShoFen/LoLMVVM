#nullable enable
using System.Windows.Input;
using LoLApp.Extensions;
using LoLApp.UI.Pages;
using MVVMToolkit;
using ViewModel;
using ViewModel.ChampionVMs;

namespace LoLApp.ViewModel;

public class EditApplicationChampionVM : ObservableObject
{
    private INavigation Navigation =>  Application.Current!.MainPage!.Navigation;
    public ChampionMgrVM MgrVM => ((App) Application.Current!).MgrVM;
    
    public EditableChampionVM? EditableChampion
    {
        get => editableChampion;
        set => SetProperty(ref editableChampion, value);
    }
    private EditableChampionVM? editableChampion;
    
    public string? Key
    {
        get => key;
        set => SetProperty(ref key, value);
    }

    private string? key;
    
    public int Value
    {
        get => value;
        set => SetProperty(ref this.value, value);
    }
    private int value;
    
    public ICommand FilePickerCommand { get; }
    public ICommand AddCharacteristicCommand { get; }
    public ICommand ValidateChampionCommand { get; }

    public EditApplicationChampionVM()
    {
        FilePickerCommand = new Command<string>(OnFilePickerCommand);
        AddCharacteristicCommand = new Command(OnAddCharacteristicCommand);
        ValidateChampionCommand = new Command(OnValidateChampionCommand);
    }
    
    private async void OnFilePickerCommand(string propertyName)
    {
        try
        {
            var options = new PickOptions
            {
                PickerTitle = "Please select a file",
                FileTypes = FilePickerFileType.Images,
            };

            var result = await FilePicker.Default.PickAsync(options);
            if (result is null) return;
            
            EditableChampion!.GetType().GetProperty(propertyName)?.SetValue(EditableChampion, await result.ToBase64());
        }
        catch (TaskCanceledException) {  /* ignored */ }
    }

    private void OnAddCharacteristicCommand()
    {
        if (Key is null) return;
        EditableChampion!.AddCharacteristic(Key, Value);
    }


    private async void OnValidateChampionCommand()
    {
        if (MgrVM.SelectedChampion is null)
        {
            _ = await MgrVM.AddChampion(EditableChampion!.ToChampionVM());
            await Navigation.PopAsync();
        }
        else
        {
            var updatedChamp = EditableChampion!.ToChampionVM();
            var updated = await MgrVM.UpdateChampion(updatedChamp);
            
            if (updated)
            {
                MgrVM.SelectedChampion = updatedChamp;
            }
            
            await Navigation.PopAsync();
        }

        if (Navigation.NavigationStack.Count == 1) await Navigation.PushAsync(new ChampionDetailPage());
    }
}
