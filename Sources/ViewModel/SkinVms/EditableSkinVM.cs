using Model;
using MVVMToolkit;
using ViewModel.ChampionVMs;

namespace ViewModel.SkinVms;

public class EditableSkinVM: ObservableObject
{
    internal SkinVM? Model { get; }

    public string? Name
    {
        get => name;
        set => SetProperty(ref name, value);
    }
    private string? name;
    
    public string? Description
    {
        get => description;
        set => SetProperty(ref description, value);
    }
    private string? description;

    public string? Icon
    {
        get => icon;
        set => SetProperty(ref icon, value);
    }
    private string? icon;

    public string? Image
    {
        get => image;
        set => SetProperty(ref image, value);
    }
    private string? image = string.Empty;

    public float? Price
    {
        get => price;
        set => SetProperty(ref price, value);
    }
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
