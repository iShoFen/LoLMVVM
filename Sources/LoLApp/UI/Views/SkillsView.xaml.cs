using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace LoLApp.UI.Views;

public partial class SkillsView : ContentView
{
    public IEnumerable<SkillVM> Skills
    {
        get => (IEnumerable<SkillVM>)GetValue(SkillsProperty);
        set => SetValue(SkillsProperty, value);
    }
    public static readonly BindableProperty SkillsProperty =
        BindableProperty.Create(nameof(Skills), typeof(IEnumerable<SkillVM>), typeof(SkillsView));

    public SkillsView()
    {
        InitializeComponent();
    }
}

