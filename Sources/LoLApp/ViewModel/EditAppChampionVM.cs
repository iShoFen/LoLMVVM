#nullable enable
using System.Collections.ObjectModel;
using System.Windows.Input;
using LoLApp.Extensions;
using LoLApp.UI.Pages;
using LoLApp.Utils;
using MVVMToolkit;
using ViewModel;
using ViewModel.ChampionVMs;
using ViewModel.Enums;
using ViewModel.SkillVms;

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
    
    public SkillVM? SelectedSkill { get; set; }
    public EditableSkillVM? EditableSkill
    {
        get => editableSkill;
        set => SetProperty(ref editableSkill, value);
    }
    private EditableSkillVM? editableSkill;
    
    // Only done cause for an unknown reason, the EnumToValyesConverter doesn't work
    public ReadOnlyCollection<SkillTypeVM> SkillTypes { get; } = new(Enum.GetValues<SkillTypeVM>()
                                                                         .Except(new[] { SkillTypeVM.Unknown }).ToList());
    
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
    public ICommand DeleteCharacteristicCommand { get; }
    public ICommand ValidateChampionCommand { get; }
    public ICommand AddSkillPageCommand { get; }
    public ICommand EditSkillPageCommand { get; }
    public ICommand DeleteSkillCommand { get; }
    public ICommand CancelSkillCommand { get; }
    public ICommand ValidateSKillCommand { get; }

    

    public EditApplicationChampionVM(ChampionMgrVM mgrVM)
    {
        MgrVM = mgrVM;
        
        FilePickerCommand = new Command<string>(OnFilePickerCommand);
        AddCharacteristicCommand = new Command(OnAddCharacteristicCommand);
        DeleteCharacteristicCommand = new Command<string>(OnDeleteCharacteristicCommand);
        ValidateChampionCommand = new Command(OnValidateChampionCommand);
        
        AddSkillPageCommand = new Command(OnAddSkillPageCommand);
        EditSkillPageCommand = new Command<SkillVM>(OnEditSkillPageCommand);
        DeleteSkillCommand = new Command<SkillVM>(OnDeleteSkillCommand);
        CancelSkillCommand = new Command(OnCancelSkillCommand);
        ValidateSKillCommand = new Command(OnValidateSkillCommand);
    }
    
    private async void OnFilePickerCommand(string propertyName)
    {
        var result = await ImageFilePicker.PickImage();
        if (result is null) return;
        
        EditableChampion!.GetType().GetProperty(propertyName)?.SetValue(EditableChampion, await result.ToBase64());
    }

    private void OnAddCharacteristicCommand()
    {
        if (Key is null) return;
        EditableChampion!.AddCharacteristic(Key, Value);
    }

    private void OnDeleteCharacteristicCommand(string key)
    {
        EditableChampion!.RemoveCharacteristic(key);
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

    private void OnAddSkillPageCommand()
    {
        EditableSkill = new EditableSkillVM();
        Shell.GoToAsync(nameof(AddSkillPage), true);
    }
    
    private void OnEditSkillPageCommand(SkillVM skillVM)
    {
        SelectedSkill = skillVM;
        EditableSkill = new EditableSkillVM(skillVM);
        Shell.GoToAsync(nameof(AddSkillPage), true);
    }

    private void OnDeleteSkillCommand(SkillVM skillVM)
    {
        EditableChampion!.RemoveSkill(skillVM);
    }

    private void OnCancelSkillCommand()
    {
        EditableSkill = null;
        SelectedSkill = null;
        Shell.GoToAsync("..", true);
    }
    
    private async void OnValidateSkillCommand()
    {
        if (SelectedSkill is null)
        {
            EditableChampion!.AddSkill(EditableSkill!.ToSkillVM());
        }
        else
        {
            _ = EditableSkill!.ToSkillVM();
        }
        
        await Shell.GoToAsync("../", true);
    }
}
