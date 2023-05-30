using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace MVVMToolkit;

/// <summary>
/// A simple implementation of an read only observable dictionary that implement <see cref="INotifyCollectionChanged"/> and <see cref="INotifyPropertyChanged"/>
/// </summary>
/// <typeparam name="TKey">The type of the key</typeparam>
/// <typeparam name="TValue">The type of the value</typeparam>
[Serializable]
public class ReadOnlyObservableDictionary<TKey, TValue> : ReadOnlyDictionary<TKey, TValue>, INotifyCollectionChanged, INotifyPropertyChanged
    where TKey : notnull
{
    /// <summary>
    /// The handler for all the collection changed events
    /// NonSerialized because we can't serialize it
    /// </summary>
    [field: NonSerialized]
    public event NotifyCollectionChangedEventHandler? CollectionChanged;
    
    /// <summary>
    /// The handler for all the property changed events
    /// NonSerialized because we can't serialize it
    /// </summary>
    [field: NonSerialized]
    public event PropertyChangedEventHandler? PropertyChanged;
    
    /// <summary>
    /// Occurs when the handled collection changes, either by adding or removing an item.
    /// </summary>
    /// <param name="e">The event arguments</param>
    protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e) 
        => CollectionChanged?.Invoke(this, e);

    /// <summary>
    /// Occurs when a property value changes, or when the handled collection property changes.
    /// </summary>
    /// <param name="args"></param>
    protected virtual void OnPropertyChanged(PropertyChangedEventArgs args) 
        => PropertyChanged?.Invoke(this, args);

    /// <summary>
    /// Initializes a new instance of the <see cref="ReadOnlyObservableDictionary{TKey,TValue}"/> class.
    /// </summary>
    /// <param name="dictionary">The dictionary to wrap</param>
    public ReadOnlyObservableDictionary(IDictionary<TKey, TValue> dictionary) : base(dictionary)
    {
        // We need to subscribe to the events of the wrapped dictionary
        ((INotifyCollectionChanged)Dictionary).CollectionChanged += HandleCollectionChanged;
        ((INotifyPropertyChanged)Dictionary).PropertyChanged += HandlePropertyChanged;
    }
    
    /// <summary>
    /// Occurs when the wrapped collection changes, either by adding or removing an item.
    /// </summary>
    /// <param name="sender">The sender</param>
    /// <param name="e">The event arguments</param>
    private void HandleCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) => OnCollectionChanged(e);

    /// <summary>
    /// Occurs when a property of the wrapped collection changes.
    /// </summary>
    /// <param name="sender">The sender</param>
    /// <param name="e">The event arguments</param>
    private void HandlePropertyChanged(object? sender, PropertyChangedEventArgs e) => OnPropertyChanged(e);
}
