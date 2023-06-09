using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.ChampionVMs;

namespace LoLApp.UI.Views;

public partial class ChampionDetailSummaryView : ContentView
{
    public ChampionVM SelectedChamp => ((App) Application.Current)!.MgrVM.SelectedChampion;
    
    public ChampionDetailSummaryView()
    {
        InitializeComponent();
        BindingContext = SelectedChamp;
    }
}

