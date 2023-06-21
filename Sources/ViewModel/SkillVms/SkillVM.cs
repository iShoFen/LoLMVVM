using Model;
using MVVMToolkit;
using ViewModel.Enums;
using ViewModel.Mappers;

namespace ViewModel.SkillVms;

public class SkillVM: ObservableObject<Skill>
{
    internal new Skill Model => base.Model;
    
    public string Name => Model.Name;

    public SkillTypeVM Type => Model.Type.ToVM();

    public string Description
    {
        get => Model.Description;
        set => SetProperty(Model.Description, value, Model, (model, value) => model.Description = value);
    }
    
    public SkillVM(Skill model) : base(model) { }
}
