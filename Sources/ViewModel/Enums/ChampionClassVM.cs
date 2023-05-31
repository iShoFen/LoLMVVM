using System.ComponentModel;

namespace ViewModel.Enums;

public enum ChampionClassVM
{
    [Image("")]
    Unknown,
    
    [Image("assassin_icon.png")]
    Assassin,
    
    [Image("fighter_icon.png")]
    Fighter,
    
    [Image("mage_icon.png")]
    Mage,
    
    [Image("marksman_icon.png")]
    Marksman,
    
    [Image("support_icon.png")]
    Support,
    
    [Image("tank_icon.png")]
    Tank,
}

public static class EnumExtension
{
    public static string GetImage(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = field?.GetCustomAttributes(typeof(ImageAttribute), false)[0] as ImageAttribute;
        
        return attribute?.ImageName ?? string.Empty;
    }
}
