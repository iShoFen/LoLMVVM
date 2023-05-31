namespace LoLApp.UI.Components;

public partial class ImageText : ContentView
{
    public string ImageSource
    {
        get => (string)GetValue(ImageSourceProperty);
        set => SetValue(ImageSourceProperty, value);
    }
    public static readonly BindableProperty ImageSourceProperty =
        BindableProperty.Create(nameof(ImageSource), typeof(string), typeof(ImageText));
    
    public Color ImageColor
    {
        get => (Color)GetValue(ImageColorProperty);
        set => SetValue(ImageColorProperty, value);
    }
    public static readonly BindableProperty ImageColorProperty =
        BindableProperty.Create(nameof(ImageColor), typeof(Color), typeof(ImageText));
    
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(ImageText));
    
    
    public ImageText()
    {
        InitializeComponent();
    }
}

