using Model;
using MVVMToolkit;

namespace ViewModel;

public class SkinVM: ObservableObject<Skin>
{
    internal new Skin Model => base.Model;
    
    public string Name => Model.Name;
    
    public ChampionVM Champion => new(Model.Champion);
    
    public string Description
    {
        get => Model.Description;
        set => SetProperty(Model.Description, value, Model, (model, value) => model.Description = value);
    }
    
    public string Icon
    {
        get => Model.Icon;
        set => SetProperty(Model.Icon, value, Model, (model, value) => model.Icon = value);
    }
    
    public LargeImage Image
    {
        get => Model.Image;
        set => SetProperty(Model.Image, value, Model, (model, value) => model.Image = value);
    }
    
    public float Price
    {
        get => Model.Price;
        set => SetProperty(Model.Price, value, Model, (model, value) => model.Price = value);
    }

    public SkinVM(Skin model) : base(model)
    {
        Champion.AddSkin(this);
    }
}
