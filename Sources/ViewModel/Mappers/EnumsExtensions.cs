using Model;
using ViewModel.Enums;

namespace ViewModel.Mappers;

internal static class ViewModelEnumExtensions
{

    ///<summary>
    /// ChampionClassVM version of ToVM
    ///</summary>
    ///<param name="model">The model</param>
    ///<returns>The view model</returns>
    public static ChampionClassVM ToVM(this ChampionClass model) => EnumsMapper.ChampionClassMapper.GetOther(model);
    
    ///<summary>
    /// SkillTypeVM version of ToVM
    ///</summary>
    ///<param name="model">The model</param>
    ///<returns>The view model</returns>
    public static SkillTypeVM ToVM(this SkillType model) => EnumsMapper.SkillTypeMapper.GetOther(model);
    
    /// <summary>
    /// SkillType version of ToModel
    /// </summary>
    /// <param name="vm">The view model</param>
    /// <returns>The model</returns>
    public static SkillType ToModel(this SkillTypeVM vm) => EnumsMapper.SkillTypeMapper.GetBase(vm);
    
    /// <summary>
    /// ChampionClass version of ToModel
    /// </summary>
    /// <param name="vm">The view model</param>
    /// <returns>The model</returns>
    public static ChampionClass ToModel(this ChampionClassVM vm) => EnumsMapper.ChampionClassMapper.GetBase(vm);
    
}
