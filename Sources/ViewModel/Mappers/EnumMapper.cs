using Model;
using Shared.Mappers;
using ViewModel.Enums;

namespace ViewModel.Mappers;

/// <summary>
/// A static class to hold the mappers
/// </summary>
internal static class EnumsMapper
{
    /// <summary>
    /// The mapper for the ChampionClass
    /// </summary>
    public static EnumsMapper<ChampionClass, ChampionClassVM> ChampionClassMapper { get; } = new(
        (ChampionClass.Unknown, ChampionClassVM.Unknown), 
        (ChampionClass.Assassin, ChampionClassVM.Assassin),
        (ChampionClass.Fighter, ChampionClassVM.Fighter),
        (ChampionClass.Mage, ChampionClassVM.Mage),
        (ChampionClass.Marksman, ChampionClassVM.Marksman),
        (ChampionClass.Support, ChampionClassVM.Support),
        (ChampionClass.Tank, ChampionClassVM.Tank)
        );
    
    /// <summary>
    /// The mapper for the SkillType
    /// </summary>
    public static EnumsMapper<SkillType, SkillTypeVM> SkillTypeMapper { get; } = new(
        (SkillType.Unknown, SkillTypeVM.Unknown),
        (SkillType.Basic, SkillTypeVM.Basic),
        (SkillType.Passive, SkillTypeVM.Passive),
        (SkillType.Ultimate, SkillTypeVM.Ultimate)
        );
}