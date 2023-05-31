namespace ViewModel.Enums;

[AttributeUsage(AttributeTargets.Field)]
public class ImageAttribute: Attribute
{
    public string ImageName { get; }
    
    public ImageAttribute(string imageName)
    {
        ImageName = imageName;
    }
}
