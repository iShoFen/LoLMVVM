using CommunityToolkit.Mvvm.ComponentModel;
using Model;
using ViewModel.Enums;
using ViewModel.Mappers;

namespace ViewModel.SkillVms;

public partial class EditableSkillVM: ObservableObject
{
    internal SkillVM? Model { get; }
    
    [ObservableProperty]
    private string? name;

    [ObservableProperty]
    private SkillTypeVM type;

    [ObservableProperty]
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
