using Model;
using MVVMToolkit;
using ViewModel.Enums;
using ViewModel.Mappers;

namespace ViewModel.SkillVms;

public class EditableSkillVM: ObservableObject
{
    internal SkillVM? Model { get; }
    
    public string? Name
    {
        get => name;
        set => SetProperty(ref name, value);
    }
    private string? name;

    public SkillTypeVM Type
    {
        get => type;
        set => SetProperty(ref type, value);
    }
    private SkillTypeVM type;

    public string? Description
    {
        get => description;
        set => SetProperty(ref description, value);
    }
    private string? description;
    
    public EditableSkillVM(SkillVM model)
    {
        Model = model;
        Name = model.Name;
        Type = model.Type;
        Description = model.Description;
    }
    
    public EditableSkillVM() {}
}

public static class EditableSkillVMExtensions
{
    public static SkillVM ToSkillVM(this EditableSkillVM vm)
    {
        if (vm.Model is not null)
        {
            vm.Model.Description = vm.Description ?? "";
            return vm.Model;
        }

        var model = new Skill(vm.Name ?? "", vm.Type.ToModel(), vm.Description ?? "");
        return new SkillVM(model);
    }
}
