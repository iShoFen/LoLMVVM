#nullable enable
using System.Windows.Input;
using LoLApp.Extensions;
using LoLApp.UI.Pages;
using LoLApp.Utils;
using MVVMToolkit;
using ViewModel;
using ViewModel.ChampionVMs;

namespace LoLApp.ViewModel;

public class EditApplicationChampionVM : ObservableObject
{
    private static Shell Shell => Shell.Current;
    public ChampionMgrVM MgrVM { get; }
    
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

    public EditApplicationChampionVM(ChampionMgrVM mgrVM)
    {
        MgrVM = mgrVM;
        FilePickerCommand = new Command<string>(OnFilePickerCommand);
        AddCharacteristicCommand = new Command(OnAddCharacteristicCommand);
        ValidateChampionCommand = new Command(OnValidateChampionCommand);
    }
    
    private async void OnFilePickerCommand(string propertyName)
    {
        await ImageFilePicker.PickImage(Callback);
        return;
        
        void Callback(FileResult? result) => EditableChampion!.GetType().GetProperty(propertyName)?.SetValue(EditableChampion, result.ToBase64());
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
            await Shell.GoToAsync("../" + nameof(ChampionDetailPage), true);
        }
        else
        {
            var updatedChamp = EditableChampion!.ToChampionVM();
            var updated = await MgrVM.UpdateChampion(updatedChamp);
            
            if (updated)
            {
                MgrVM.SelectedChampion = updatedChamp;
            }
            
            await Shell.GoToAsync("..", true);
        }
    }
}
