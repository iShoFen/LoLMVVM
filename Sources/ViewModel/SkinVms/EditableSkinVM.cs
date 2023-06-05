using MVVMToolkit;

namespace ViewModel.SkinVms;

public class EditableSkinVM: ObservableObject
{
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
        name = model.Name;
        description = model.Description;
        icon = model.Icon;
        image = model.Image;
        price = model.Price;
    }
}
