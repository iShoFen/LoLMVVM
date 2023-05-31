using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoLApp.UI.Views;

public partial class ChampionDetailSummary : ContentView
{
    public string Base64Image
    {
        get => (string)GetValue(Base64ImageProperty);
        set => SetValue(Base64ImageProperty, value);
    }
    public static readonly BindableProperty Base64ImageProperty =
        BindableProperty.Create(nameof(Base64Image), typeof(string), typeof(ChampionDetailSummary));
    
    public string Name 
    {
        get => (string)GetValue(NameProperty);
        set => SetValue(NameProperty, value);
    }
    public static readonly BindableProperty NameProperty =
        BindableProperty.Create(nameof(Name), typeof(string), typeof(ChampionDetailSummary));
    
    public string Class 
    {
        get => (string)GetValue(ClassProperty);
        set => SetValue(ClassProperty, value);
    }
    public static readonly BindableProperty ClassProperty =
        BindableProperty.Create(nameof(Class), typeof(string), typeof(ChampionDetailSummary));
    
    public string ClassImage
    {
        get => (string)GetValue(ClassImageProperty);
        set => SetValue(ClassImageProperty, value);
    }
    public static readonly BindableProperty ClassImageProperty =
        BindableProperty.Create(nameof(ClassImage), typeof(string), typeof(ChampionDetailSummary));

    public Color ClassColor
    {
        get => (Color)GetValue(ClassColorProperty);
        set => SetValue(ClassColorProperty, value);
    }
    public static readonly BindableProperty ClassColorProperty =
        BindableProperty.Create(nameof(ClassColor), typeof(Color), typeof(ChampionDetailSummary));
    public string Bio
    {
        get => (string)GetValue(BioProperty);
        set => SetValue(BioProperty, value);
    }
    public static readonly BindableProperty BioProperty =
        BindableProperty.Create(nameof(Bio), typeof(string), typeof(ChampionDetailSummary));

    public ChampionDetailSummary()
    {
        InitializeComponent();
    }
}

