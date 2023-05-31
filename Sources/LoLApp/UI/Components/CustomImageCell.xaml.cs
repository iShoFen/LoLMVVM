using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoLApp.UI.Components;

public partial class CustomImageCell : ViewCell
{
    public string Base64Image
    {
        get => (string)GetValue(Base64ImageProperty);
        set => SetValue(Base64ImageProperty, value);
    }
    public static readonly BindableProperty Base64ImageProperty =
        BindableProperty.Create(nameof(Base64Image), typeof(string), typeof(CustomImageCell));
    
    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }
    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create(nameof(Title), typeof(string), typeof(CustomImageCell));
    
    public string Subtitle
    {
        get => (string)GetValue(SubtitleProperty);
        set => SetValue(SubtitleProperty, value);
    }
    public static readonly BindableProperty SubtitleProperty =
        BindableProperty.Create(nameof(Subtitle), typeof(string), typeof(CustomImageCell));
    
    public CustomImageCell()
    {
        InitializeComponent();
    }
}

