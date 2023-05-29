using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace MVVMToolkit;

/// <summary>
/// Create a base class for all ViewModel with observable properties
/// </summary>
public class ObservableObject: INotifyPropertyChanged
{
    /// <summary>
    /// The event raised when a property changes
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Raise the PropertyChanged event
    /// </summary>
    /// <param name="propertyName">The name of the property that changed</param>
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// Set the value of a property and raise the <see cref="PropertyChanged"/> event if the value has changed
    /// First we take the value of the field as reference, to be sure this is not a copy even if the field is a value type
    /// We also add the <see cref="NotNullIfNotNullAttribute"/> attribute to be sure that if the new value is null, the field will be nullable
    /// Now we juste check if the field is equal to the new value, if it is, we return false and not fire the PropertyChanged event
    /// Otherwise we set the field to the new value, and fire the <see cref="PropertyChanged"/> event then return true
    /// </summary>
    /// <param name="field">The field to set</param>
    /// <param name="newValue">The new value</param>
    /// <param name="comparer">The comparer to use to compare the old and new values</param>
    /// <param name="propertyName">The name of the property that changed (not needed to be specified with the <see cref="CallerMemberNameAttribute"/>)</param>
    /// <typeparam name="T">The type of the property</typeparam>
    /// <returns>True if the value has changed, false otherwise</returns>
    protected bool SetProperty<T>([NotNullIfNotNull(nameof(newValue))]ref T field, T newValue, IEqualityComparer<T> comparer, [CallerMemberName] string? propertyName = null)
    {
        ArgumentNullException.ThrowIfNull(comparer);
        
        if (comparer.Equals(field, newValue)) return false;
        
        field = newValue;
        
        OnPropertyChanged(propertyName);
        
        return true;
    }
    
    /// <summary>
    /// Set the value of a property and raise the PropertyChanged event if the value has changed
    /// See <see cref="SetProperty{T}(ref T,T,IEqualityComparer{T},string?)"/> for more details
    /// </summary>
    /// <param name="field">The field to set</param>
    /// <param name="newValue">The new value</param>
    /// <param name="propertyName">The name of the property that changed (not needed to be specified with the <see cref="CallerMemberNameAttribute"/>)</param>
    /// <typeparam name="T">The type of the property</typeparam>
    /// <returns>True if the value has changed, false otherwise</returns>
    protected bool SetProperty<T>([NotNullIfNotNull(nameof(newValue))]ref T field, T newValue, [CallerMemberName] string? propertyName = null)
        => SetProperty(ref field, newValue, EqualityComparer<T>.Default, propertyName);

    /// <summary>
    /// Set the value of a property and raise the <see cref="PropertyChanged"/> event if the value has changed
    /// This method is a bit different than <see cref="SetProperty{T}(ref T,T,IEqualityComparer{T},string?)"/> because it take a callback to set the value of the property
    /// Its used when we cannot access the field directly, for example when you use the property of a model you will not store the data in the VM but use the model.
    /// First we need to take the old value of the property, then we check if the old value is equal to the new value if it is we return false and not fire the <see cref="PropertyChanged"/> event
    /// If not, we call the callback with the new value and the model, this callback represent the way to set the value of the property in the model
    /// Then we fire the <see cref="PropertyChanged"/> event and return true
    /// <code>
    /// public class Champion
    /// {
    ///     public string Name { get; set; }
    ///     ....
    /// }
    /// </code>
    /// So in the model we have a property Name, and we want to bind it to the VM, we can do it like this:
    /// <code>
    /// public class ChampionVM : ObservableObject
    /// {
    ///     public Champion Model { get; }
    ///
    ///     public ChampionVM(Champion model)
    ///     {
    ///         Model = model;
    ///     }
    ///
    ///     public string Name
    ///     {
    ///         get => Model.Name;
    ///         set => Set(Model.Name, value, Model, (model, name) => model.Name = name);
    ///     }
    /// }
    /// </code>
    /// We can see the we use the Model.Name as the old value, and for the callback we only set the Name property of the model to the new value
    /// </summary>
    /// <param name="oldValue">The old value of the property</param>
    /// <param name="newValue">The new value of the property</param>
    /// <param name="comparer">The comparer to use to compare the old and new values</param>
    /// <param name="model">The model to set the value of the property</param>
    /// <param name="callback">The callback to set the value of the property in the model</param>
    /// <param name="propertyName">The name of the property that changed (not needed to be specified with the <see cref="CallerMemberNameAttribute"/>)</param>
    /// <typeparam name="TModel">The type of the model</typeparam>
    /// <typeparam name="T">The type of the property</typeparam>
    /// <returns>True if the value has changed, false otherwise</returns>
    protected bool SetProperty<TModel, T>(T oldValue, T newValue, IEqualityComparer<T> comparer, TModel model, Action<TModel, T> callback, [CallerMemberName] string? propertyName = null)
        where TModel : class
    {
        ArgumentNullException.ThrowIfNull(comparer);
        ArgumentNullException.ThrowIfNull(model);
        ArgumentNullException.ThrowIfNull(callback);

        if (comparer.Equals(oldValue, newValue)) return false;
        
        callback(model, newValue);

        OnPropertyChanged(propertyName);

        return true;
    }
    
    /// <summary>
    /// Set the value of a property and raise the PropertyChanged event if the value has changed
    /// See <see cref="SetProperty{TModel,T}(T,T,IEqualityComparer{T},TModel,Action{TModel,T},string?)"/> for more details
    /// </summary>
    /// <param name="oldValue">The old value of the property</param>
    /// <param name="newValue">The new value of the property</param>
    /// <param name="model">The model to set the value of the property</param>
    /// <param name="callback">The callback to set the value of the property in the model</param>
    /// <param name="propertyName">The name of the property that changed (not needed to be specified with the <see cref="CallerMemberNameAttribute"/>)</param>
    /// <typeparam name="TModel">The type of the model</typeparam>
    /// <typeparam name="T">The type of the property</typeparam>
    /// <returns>True if the value has changed, false otherwise</returns>
    protected bool SetProperty<TModel, T>(T oldValue, T newValue, TModel model, Action<TModel, T> callback, [CallerMemberName] string? propertyName = null)
        where TModel : class => SetProperty(oldValue, newValue, EqualityComparer<T>.Default, model, callback, propertyName);
}