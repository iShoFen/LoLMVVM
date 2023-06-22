﻿using ViewModel.ChampionVMs;

namespace LoLApp.UI.Views;

public partial class ChampionDetailSummaryView : ContentView
{
    public ChampionVM SelectedChamp
    {
        get => (ChampionVM) GetValue(SelectedChampProperty);
        set => SetValue(SelectedChampProperty, value);
    }
    public static readonly BindableProperty SelectedChampProperty =
        BindableProperty.Create(nameof(SelectedChamp), typeof(ChampionVM), typeof(ChampionDetailSummaryView));
    
    public ChampionDetailSummaryView()
    {
        InitializeComponent();
    }
}

