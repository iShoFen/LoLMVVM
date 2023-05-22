using System.ComponentModel;
using System.Runtime.CompilerServices;
using Model;

namespace ViewModel;

public class ChampionVM: INotifyPropertyChanged
{
    public Champion Model
    {
        get => _model;
        set
        {
            if (_model.Equals(value)) return;
            
            _model = value;
            OnPropertyChanged();
        }
    }
    private Champion _model;

    public string Name => Model.Name;

    public string Icon
    {
        get => Model.Icon;
        set
        {
            if(Model.Icon.Equals(value)) return;
            
            Model.Icon = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    
    public ChampionVM(Champion model)
    {
        _model = model;
    }
}
