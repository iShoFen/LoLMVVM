using Model;
using MVVMToolkit;

namespace ViewModel;

public class ChampionVM: ObservableObject<Champion>
{
    
    public string Name => Model.Name;

    public string Bio
    {
        get => Model.Bio;
        set => SetProperty(Model.Bio, value, Model, (model, value) => model.Bio = value);
    }

    public string Icon
    {
        get => Model.Icon;
        set => SetProperty(Model.Icon, value, Model, (model, value) => model.Icon = value);
    }
    
    public LargeImage Image
    {
        get => Model.Image;
        set => SetProperty(Model.Image, value, Model ,(model, value) => model.Image = value);
    }
    
    public ChampionClass Class
    {
        get => Model.Class;
        set => SetProperty(Model.Class, value, Model, (model, value) => model.Class = value);
    }
    
    public ChampionVM(Champion model) : base(model) { }
}
