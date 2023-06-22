#nullable enable
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LoLApp.Extensions;
using LoLApp.UI.Pages;
using LoLApp.Utils;
using ViewModel;
using ViewModel.SkinVms;

namespace LoLApp.ViewModel;

public partial class EditApplicationSkinVM: ObservableObject
{
    private static Shell Shell => Shell.Current;
    public ChampionMgrVM MgrVM { get; }
    
    [ObservableProperty]
    private EditableSkinVM? editableSkin;

    public EditApplicationSkinVM(ChampionMgrVM mgrVM) => MgrVM = mgrVM;

    [RelayCommand]
    private async Task FilePicker(string propertyName)
    {
        var result = await ImageFilePicker.PickImage();
        if (result is null) return;
        EditableSkin!.GetType().GetProperty(propertyName)?.SetValue(EditableSkin, await result.ToBase64());
    }
    
    [RelayCommand]
    private async Task ValidateSkin()
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
