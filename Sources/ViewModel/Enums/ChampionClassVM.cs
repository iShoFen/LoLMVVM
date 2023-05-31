using System.ComponentModel;

namespace ViewModel.Enums;

public enum ChampionClassVM
{
    [Image("")]
    Unknown,
    
    [Image("assassin.png")]
    Assassin,
    
    [Image("fighter.png")]
    Fighter,
    
    [Image("mage.png")]
    Mage,
    
    [Image("marksman.png")]
    Marksman,
    
    [Image("support.png")]
    Support,
    
    [Image("tank.png")]
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
