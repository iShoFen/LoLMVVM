using CommunityToolkit.Mvvm.ComponentModel;
using Model;
using ViewModel.ChampionVMs;

namespace ViewModel.SkinVms;

public partial class EditableSkinVM: ObservableObject
{
    internal SkinVM? Model { get; }

    [ObservableProperty]
    private string? name;
    
    [ObservableProperty]
    private string? description;

    [ObservableProperty]
    private string? icon;

    [ObservableProperty]
    private string? image = string.Empty;

    [ObservableProperty]
    private float? price;
    
    public EditableSkinVM() { }
    
    public EditableSkinVM(SkinVM model)
    {
        Model = model;
        name = Model.Name;
        description = Model.Description;
        icon = Model.Icon;
        image = Model.Image;
        price = Model.Price;
    }
}

public static class EditableSKinVMExtension
{
    public static SkinVM ToSkinVM(this EditableSkinVM vm, ChampionVM champion)
    {
        if (vm.Model != null)
        {
            vm.Model.Description = vm.Description ?? "";
            vm.Model.Icon = vm.Icon ?? "";
            vm.Model.Image = vm.Image ?? "";
            vm.Model.Price = vm.Price ?? 0F;
            
            return vm.Model;
        }
        
        var model = new Skin(vm.Name ?? "", champion.Model, vm.Price ?? 0F, vm.Icon ?? "", vm.Image ?? "", vm.Description ?? "");
        return new SkinVM(model, champion);
    }

}
