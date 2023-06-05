using Model;
using MVVMToolkit;
using ViewModel.ChampionVMs;

namespace ViewModel.SkinVms;

public class SkinVM: ObservableObject<Skin>
{
    internal new Skin Model => base.Model;
    
    public string Name => Model.Name;
    
    public ChampionVM Champion { get; internal init; } = null!;

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
    
    public string Image
    {
        get => Model.Image.Base64;
        set => SetProperty(Model.Image.Base64, value, Model, (model, value) => model.Image.Base64 = value);
    }
    
    public float Price
    {
        get => Model.Price;
        set => SetProperty(Model.Price, value, Model, (model, value) => model.Price = value);
    }

    public SkinVM(Skin model, ChampionVM champion) : base(model)
    {
        Champion = champion;
        Champion.AddSkin(this);
    }
    
    internal SkinVM(Skin model) : base(model) { }
}
