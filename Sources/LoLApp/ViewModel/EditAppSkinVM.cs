#nullable enable
using System.Windows.Input;
using LoLApp.Extensions;
using LoLApp.UI.Pages;
using LoLApp.Utils;
using MVVMToolkit;
using ViewModel;
using ViewModel.SkinVms;

namespace LoLApp.ViewModel;

public class EditApplicationSkinVM: ObservableObject
{
    private static Shell Shell => Shell.Current;
    public ChampionMgrVM MgrVM { get; }
    
    public EditableSkinVM? EditableSkin
    {
        get => editableSkin;
        set => SetProperty(ref editableSkin, value);
    }
    private EditableSkinVM? editableSkin;
    public ICommand FilePickerCommand { get; }
    public ICommand ValidateSkinCommand { get; }

    public EditApplicationSkinVM(ChampionMgrVM mgrVM)
    {
        MgrVM = mgrVM;
        FilePickerCommand = new Command<string>(OnFilePickerCommand);
        ValidateSkinCommand = new Command(OnValidateSkinCommand);
    }
    
    private async void OnFilePickerCommand(string propertyName)
    {
        var result = ImageFilePicker.PickImage().Result;
        if (result is null) return;
        EditableSkin!.GetType().GetProperty(propertyName)?.SetValue(EditableSkin, result.ToBase64());
    }
    
    private async void OnValidateSkinCommand()
    {
        if (MgrVM.SelectedSkin is null)
        {
            _ = await MgrVM.AddSkin(EditableSkin!.ToSkinVM(MgrVM.SelectedChampion!));
            await Shell.GoToAsync("../" + nameof(SkinDetailPage), true);
        }
        else
        {
            var updatedSkin = EditableSkin!.ToSkinVM(MgrVM.SelectedChampion!);
            var updated = await MgrVM.UpdateSkin(updatedSkin);
            
            if (updated)
            {
                MgrVM.SelectedSkin = updatedSkin;
            }
            
            await Shell.GoToAsync("..", true);
        }
    }
}
