using Model;
using MVVMToolkit;

namespace ViewModel;

public class SkillVM: ObservableObject<Skill>
{
    internal new Skill Model => base.Model;
    
    public string Name => Model.Name;
    
    public SkillType Type => Model.Type;

    public string Description
    {
        get => Model.Description;
        set => SetProperty(Model.Description, value, Model, (model, value) => model.Description = value);
    }
    
    public SkillVM(Skill model) : base(model) { }
}
