using System.Collections.ObjectModel;
using Model;
using MVVMToolkit;
using ViewModel.Enums;
using ViewModel.Mappers;
using ViewModel.SkillVms;
using ViewModel.SkinVms;

namespace ViewModel.ChampionVMs;

public class ChampionVM: ObservableObject<Champion>
{
    internal new Champion Model => base.Model;
    
    public string Name => Model.Name;

    public string Bio
    {
        get => Model.Bio;
        private set => SetProperty(Model.Bio, value, Model, (model, value) => model.Bio = value);
    }

    public string Icon
    {
        get => Model.Icon;
        private set => SetProperty(Model.Icon, value, Model, (model, value) => model.Icon = value);
    }
    
    public string Image
    {
        get => Model.Image.Base64;
        private set => SetProperty(Model.Image.Base64, value, Model ,(model, value) => model.Image.Base64 = value);
    }
    
    public ChampionClassVM Class
    {
        get => Model.Class.ToVM();
        private set => SetProperty(Model.Class,  value.ToModel(), Model, (model, value) => model.Class = value);
    }
    
    public string ClassImage => Class.GetImage();

    public ReadOnlyObservableCollection<SkinVM> Skins { get; private set; }
    private readonly ObservableCollection<SkinVM> skins;

    public ReadOnlyObservableDictionary<string, int> Characteristics { get; private set; }
    private readonly ObservableDictionary<string, int> characteristics;
    
    public ReadOnlyObservableCollection<SkillVM> Skills { get; private set; }
    private readonly ObservableCollection<SkillVM> skills;

    public ChampionVM(Champion model) : base(model)
    {
        skins = new ObservableCollection<SkinVM>(model.Skins.Select(skin => new SkinVM(skin)
                                                     {
                                                         Champion = this
                                                     }
                                                 )
        );
        Skins = new ReadOnlyObservableCollection<SkinVM>(skins);
        
        characteristics = new ObservableDictionary<string, int>(model.Characteristics);
        Characteristics = new ReadOnlyObservableDictionary<string, int>(characteristics);
        
        skills = new ObservableCollection<SkillVM>(model.Skills.Select(skill => new SkillVM(skill)));
        Skills = new ReadOnlyObservableCollection<SkillVM>(skills);
    }
    
    internal bool AddSkin(SkinVM skin)
    {
        skins.Add(skin);
        
        return Model.AddSkin(skin.Model);
    }
    
    internal bool RemoveSkin(SkinVM skin)
    {
        if (!Model.RemoveSkin(skin.Model)) return false;
        
        skins.Remove(skin);
        
        return true;
    }
    
    public bool AddSkill(SkillVM skill)
    {
        if (!Model.AddSkill(skill.Model)) return false;
        
        skills.Add(skill);
        
        return true;
    }
    
    public bool RemoveSkill(SkillVM skill)
    {
        if (!Model.RemoveSkill(skill.Model)) return false;
        
        skills.Remove(skill);
        
        return true;
    }
    
    public void AddCharacteristics(params Tuple<string, int>[] someCharacteristics)
    {
        Model.AddCharacteristics(someCharacteristics);
        foreach (var chara in someCharacteristics)
        {
            characteristics[chara.Item1] = chara.Item2;
        }
    }
    
    public bool RemoveCharacteristic(string label)
    {
        if (!characteristics.ContainsKey(label)) return false;
        
        characteristics.Remove(label);
        
        return true;
    }
}
