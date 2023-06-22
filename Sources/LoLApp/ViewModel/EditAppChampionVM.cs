#nullable enable
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LoLApp.Extensions;
using LoLApp.UI.Pages;
using LoLApp.Utils;
using ViewModel;
using ViewModel.ChampionVMs;
using ViewModel.Enums;
using ViewModel.SkillVms;

namespace LoLApp.ViewModel;

public partial class EditApplicationChampionVM : ObservableObject
{
    private static Shell Shell => Shell.Current;
    public ChampionMgrVM MgrVM { get; }
    
    [ObservableProperty]
    private EditableChampionVM? editableChampion;
    
    public SkillVM? SelectedSkill { get; set; }
    
    [ObservableProperty]
    private EditableSkillVM? editableSkill;
    
    // Only done cause for an unknown reason, the EnumToValyesConverter doesn't work
    public ReadOnlyCollection<SkillTypeVM> SkillTypes { get; } = new(Enum.GetValues<SkillTypeVM>()
                                                                         .Except(new[] { SkillTypeVM.Unknown }).ToList());
    
    [ObservableProperty]
    private string? key;
    
    [ObservableProperty]
    private int value;
    
    public EditApplicationChampionVM(ChampionMgrVM mgrVM) => MgrVM = mgrVM;

    [RelayCommand]
    private async Task FilePicker(string propertyName)
    {
        var result = await ImageFilePicker.PickImage();
        if (result is null) return;
        
        EditableChampion!.GetType().GetProperty(propertyName)?.SetValue(EditableChampion, await result.ToBase64());
    }

    [RelayCommand]
    private void AddCharacteristic()
    {
        if (Key is null) return;
        EditableChampion!.AddCharacteristic(Key, Value);
    }

    [RelayCommand]
    private void DeleteCharacteristic(string charKey)
    {
        EditableChampion!.RemoveCharacteristic(charKey);
    }

    [RelayCommand]
    private async Task ValidateChampion()
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

    [RelayCommand]
    private void AddSkillPage()
    {
        EditableSkill = new EditableSkillVM();
        Shell.GoToAsync(nameof(AddSkillPage), true);
    }
    
    [RelayCommand]
    private void EditSkillPage(SkillVM skillVM)
    {
        SelectedSkill = skillVM;
        EditableSkill = new EditableSkillVM(skillVM);
        Shell.GoToAsync(nameof(AddSkillPage), true);
    }

    [RelayCommand]
    private void DeleteSkill(SkillVM skillVM)
    {
        EditableChampion!.RemoveSkill(skillVM);
    }

    [RelayCommand]
    private void CancelSkill()
    {
        EditableSkill = null;
        SelectedSkill = null;
        Shell.GoToAsync("..", true);
    }
    
    [RelayCommand]
    private async Task ValidateSkill()
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
