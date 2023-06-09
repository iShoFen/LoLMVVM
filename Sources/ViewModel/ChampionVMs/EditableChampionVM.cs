using System.Collections.ObjectModel;
using Model;
using MVVMToolkit;
using ViewModel.Enums;
using ViewModel.Mappers;
using ViewModel.SkinVms;

namespace ViewModel.ChampionVMs;

public class EditableChampionVM: ObservableObject
{
    public string? Name
    {
        get => name;
        set => SetProperty(ref name, value);
    } 
    private string? name;

    public string? Bio
    {
        get => bio;
        set => SetProperty(ref bio, value);
    }
    private string? bio;

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
    private string? image;
    
    public ChampionClassVM Class
    {
        get => @class;
        set => SetProperty(ref @class, value);
    }
    private ChampionClassVM @class;
    
    public ReadOnlyObservableCollection<EditableSkinVM> Skins { get; }
    private readonly ObservableCollection<EditableSkinVM> skins;

    public ReadOnlyObservableDictionary<string, int> Characteristics { get; }
    private readonly ObservableDictionary<string, int> characteristics;

    public ReadOnlyObservableCollection<SkillVM> Skills { get; }
    private readonly ObservableCollection<SkillVM> skills;
    
    public EditableChampionVM()
    {
        skins = new ObservableCollection<EditableSkinVM>();
        Skins = new ReadOnlyObservableCollection<EditableSkinVM>(skins);

        skills = new ObservableCollection<SkillVM>();
        Skills = new ReadOnlyObservableCollection<SkillVM>(skills);

        characteristics = new ObservableDictionary<string, int>();
        Characteristics = new ReadOnlyObservableDictionary<string, int>(characteristics);
    }

    public EditableChampionVM(ChampionVM model)
    {
        Name = model.Name;
        Bio = model.Bio;
        Icon = model.Icon;
        Image = model.Image;
        Class = model.Class;
        
        skins = new ObservableCollection<EditableSkinVM>(model.Skins.Select(skin => new EditableSkinVM(skin)));
        Skins = new ReadOnlyObservableCollection<EditableSkinVM>(skins);

        skills = new ObservableCollection<SkillVM>(model.Skills);
        Skills = new ReadOnlyObservableCollection<SkillVM>(skills);

        characteristics = new ObservableDictionary<string, int>(model.Characteristics);
        Characteristics = new ReadOnlyObservableDictionary<string, int>(characteristics);
    }

    public void AddSkill(SkillVM skill)
    {
        skills.Add(skill);
    }
    
    public void AddCharacteristic(string key, int value)
    {
        characteristics[key] = value;
    }
    
    public void RemoveSkill(SkillVM skill)
    {
        skills.Remove(skill);
    }
    
    public void RemoveCharacteristic(string key)
    {
        characteristics.Remove(key);
    }
}

public static class EditableChampionVMExtension
{
    public static ChampionVM ToChampionVM(this EditableChampionVM vm)
    {
        var model = new Champion(vm.Name ?? "", (ChampionClass) vm.Class, vm.Icon ?? "", vm.Image ?? "", vm.Bio ?? "");
        model.AddCharacteristics(vm.Characteristics.Select(pair => Tuple.Create(pair.Key, pair.Value)).ToArray());
        foreach (var vmSkill in vm.Skills)
        {
            model.AddSkill(new Skill(vmSkill.Name, vmSkill.Type.ToModel(), vmSkill.Description));
        }

        foreach (var vmSkin in vm.Skins)
        {
            _ = new Skin(vmSkin.Name ?? "", model, vmSkin.Price ?? 0, vmSkin.Icon ?? "", vmSkin.Image ?? "", vmSkin.Description ?? "");
        }
        
        return new ChampionVM(model);
    }
}
