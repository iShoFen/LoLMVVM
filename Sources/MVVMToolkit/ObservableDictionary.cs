using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace MVVMToolkit;

/// <summary>
/// A simple implementation of an observable dictionary that implement <see cref="INotifyCollectionChanged"/> and <see cref="INotifyPropertyChanged"/>
/// </summary>
/// <typeparam name="TKey">The type of the key</typeparam>
/// <typeparam name="TValue">The type of the value</typeparam>
[Serializable]
public class ObservableDictionary<TKey, TValue>: Dictionary<TKey, TValue>, INotifyCollectionChanged, INotifyPropertyChanged
    where TKey: notnull
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
    /// Occurs when the collection changes, either by adding or removing an item.
    /// </summary>
    /// <param name="e">The event arguments</param>
    protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e) 
        => CollectionChanged?.Invoke(this, e);

    /// <summary>
    /// Occurs when a property value changes.
    /// </summary>
    /// <param name="propertyName">The name of the property that changed.</param>
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) 
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    
    /// <summary>
    /// Create a new <see cref="NotifyCollectionChangedEventArgs"/> for the <see cref="NotifyCollectionChangedAction.Add"/> action
    /// </summary>
    /// <param name="key">The key of the added item</param>
    /// <param name="value">The value of the added item</param>
    /// <returns>The <see cref="NotifyCollectionChangedEventArgs"/> for the <see cref="NotifyCollectionChangedAction.Add"/> action</returns>
    protected static NotifyCollectionChangedEventArgs AddCollectionEventArgs(TKey key, TValue value)
        => new(NotifyCollectionChangedAction.Add, new KeyValuePair<TKey, TValue>(key, value));
    
    /// <summary>
    /// Create a new <see cref="NotifyCollectionChangedEventArgs"/> for the <see cref="NotifyCollectionChangedAction.Remove"/> action
    /// </summary>
    /// <param name="key">The key of the removed item</param>
    /// <returns>The <see cref="NotifyCollectionChangedEventArgs"/> for the <see cref="NotifyCollectionChangedAction.Remove"/> action</returns>
    protected static NotifyCollectionChangedEventArgs RemoveCollectionEventArgs(TKey key)
        => new(NotifyCollectionChangedAction.Remove, key);
    
    /// <summary>
    /// Create a new <see cref="NotifyCollectionChangedEventArgs"/> for the <see cref="NotifyCollectionChangedAction.Replace"/> action
    /// </summary>
    /// <param name="key">The key of the replaced item</param>
    /// <param name="newValue">The new value of the replaced item</param>
    /// <param name="oldValue">The old value of the replaced item</param>
    /// <returns>The <see cref="NotifyCollectionChangedEventArgs"/> for the <see cref="NotifyCollectionChangedAction.Replace"/> action</returns>
    protected static NotifyCollectionChangedEventArgs ReplaceCollectionEventArgs(TKey key, TValue newValue, TValue oldValue)
        => new(NotifyCollectionChangedAction.Replace,
            new KeyValuePair<TKey, TValue>(key, newValue),
            new KeyValuePair<TKey, TValue>(key, oldValue));
    
    /// <summary>
    /// Create a new <see cref="NotifyCollectionChangedEventArgs"/> for the <see cref="NotifyCollectionChangedAction.Reset"/> action
    /// </summary>
    /// <returns>The <see cref="NotifyCollectionChangedEventArgs"/> for the <see cref="NotifyCollectionChangedAction.Reset"/> action</returns>
    protected static NotifyCollectionChangedEventArgs ResetCollectionEventArgs()
        => new(NotifyCollectionChangedAction.Reset);
    
    /// <summary>
    /// Initializes a new instance of the <see cref="ObservableDictionary{TKey,TValue}"/> class.
    /// </summary>
    /// <param name="info">The <see cref="SerializationInfo"/> that holds all the data needed to serialize or deserialize an object.</param>
    /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
    protected ObservableDictionary(SerializationInfo info, StreamingContext context): base(info, context) { }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="ObservableDictionary{TKey,TValue}"/>  class that is empty,
    /// has the default initial capacity, and uses the default equality comparer for the key type.
    /// </summary>
    public ObservableDictionary() { }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="ObservableDictionary{TKey,TValue}"/> class that is empty,
    /// has the specified initial capacity, and uses the default equality comparer for the key type.
    /// </summary>
    /// <param name="capacity">The initial number of elements that the <see cref="ObservableDictionary{TKey,TValue}"/> can contain.</param>
    public ObservableDictionary(int capacity): base(capacity) { }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="ObservableDictionary{TKey,TValue}"/> class that is empty,
    /// has the default initial capacity, and uses the specified <see cref="IEqualityComparer{T}"/>.
    /// </summary>
    /// <param name="comparer">The <see cref="IEqualityComparer{T}"/> implementation to use when comparing keys,
    /// or null to use the default <see cref="EqualityComparer{T}"/> for the type of the key.</param>
    public ObservableDictionary(IEqualityComparer<TKey> comparer): base(comparer) { }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="ObservableDictionary{TKey,TValue}"/> class that is empty,
    /// has the specified initial capacity, and uses the specified <see cref="IEqualityComparer{T}"/>.
    /// </summary>
    /// <param name="capacity">The initial number of elements that the <see cref="ObservableDictionary{TKey,TValue}"/> can contain.</param>
    /// <param name="comparer">The <see cref="IEqualityComparer{T}"/> implementation to use when comparing keys,
    /// or null to use the default <see cref="EqualityComparer{T}"/> for the type of the key.</param>
    public ObservableDictionary(int capacity, IEqualityComparer<TKey> comparer): base(capacity, comparer) { }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="ObservableDictionary{TKey,TValue}"/> class that contains elements copied from the specified <see cref="IDictionary{TKey,TValue}"/>
    /// and uses the default equality comparer for the key type.
    /// </summary>
    /// <param name="dictionary">The <see cref="IDictionary{TKey,TValue}"/> whose elements are copied to the new <see cref="ObservableDictionary{TKey,TValue}"/>.</param>
    public ObservableDictionary(IDictionary<TKey, TValue> dictionary): base(dictionary) { }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="ObservableDictionary{TKey,TValue}"/> class that contains elements copied from the specified <see cref="IDictionary{TKey,TValue}"/>
    /// and uses the specified <see cref="IEqualityComparer{T}"/>.
    /// </summary>
    /// <param name="dictionary">The <see cref="IDictionary{TKey,TValue}"/> whose elements are copied to the new <see cref="ObservableDictionary{TKey,TValue}"/>.</param>
    /// <param name="comparer">The <see cref="IEqualityComparer{T}"/> implementation to use when comparing keys,
    /// or null to use the default <see cref="EqualityComparer{T}"/> for the type of the key.</param>
    public ObservableDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
        : base(dictionary, comparer) { }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="ObservableDictionary{TKey,TValue}"/> class that contains elements copied from the specified <see cref="IEnumerable{T}"/>
    /// </summary>
    /// <param name="collection">The <see cref="IEnumerable{T}"/> whose elements are copied to the new <see cref="ObservableDictionary{TKey,TValue}"/>.</param>
    public ObservableDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection): base(collection) { }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="ObservableDictionary{TKey,TValue}"/> class that contains elements copied from the specified <see cref="IEnumerable{T}"/>
    /// and uses the specified <see cref="IEqualityComparer{T}"/>.
    /// </summary>
    /// <param name="collection">The <see cref="IEnumerable{T}"/> whose elements are copied to the new <see cref="ObservableDictionary{TKey,TValue}"/>.</param>
    /// <param name="comparer">The <see cref="IEqualityComparer{T}"/> implementation to use when comparing keys,
    /// or null to use the default <see cref="EqualityComparer{T}"/> for the type of the key.</param>
    public ObservableDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey> comparer)
        : base(collection, comparer) { }
    
    /// <summary>
    /// Adds the specified key and value to the dictionary.
    /// Hide the base Add method to raise the <see cref="CollectionChanged"/> event
    /// </summary>
    /// <param name="key">The key of the element to add.</param>
    /// <param name="value">The value of the element to add. The value can be null for reference types.</param>
    public new void Add(TKey key, TValue value)
    {
        base.Add(key, value);
        
        OnCollectionChanged(AddCollectionEventArgs(key, value));
        OnPropertyChanged(nameof(Count));
        OnPropertyChanged(nameof(Keys));
        OnPropertyChanged(nameof(Values));
    }
    
    /// <summary>
    /// Attempts to add the specified key and value to the dictionary.
    /// Hide the base TryAdd method to raise the <see cref="CollectionChanged"/> event
    /// </summary>
    /// <param name="key">The key of the element to add.</param>
    /// <param name="value">The value of the element to add. It can be null.</param>
    /// <returns>true if the key/value pair was added to the dictionary successfully; otherwise, false.</returns>
    public new bool TryAdd(TKey key, TValue value)
    {
        if (!base.TryAdd(key, value)) return false;
        
        OnCollectionChanged(AddCollectionEventArgs(key, value));
        OnPropertyChanged(nameof(Count));
        OnPropertyChanged(nameof(Keys));
        OnPropertyChanged(nameof(Values));
        
        return true;
    }
    
    /// <summary>
    /// Removes all keys and values from the <see cref="ObservableDictionary{TKey,TValue}"/>.
    /// Hide the base Clear method to raise the <see cref="CollectionChanged"/> event
    /// </summary>
    public new void Clear()
    {
        base.Clear();
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        
        OnPropertyChanged(nameof(Count));
        OnPropertyChanged(nameof(Keys));
        OnPropertyChanged(nameof(Values));
    }

    /// <summary>
    /// Removes the value with the specified key from the <see cref="ObservableDictionary{TKey,TValue}"/>.
    /// Hide the base Remove method to raise the <see cref="CollectionChanged"/> event
    /// </summary>
    /// <param name="key">The key of the element to remove.</param>
    /// <returns>true if the element is successfully found and removed; otherwise, false. This method returns false if key is not found in the <see cref="ObservableDictionary{TKey,TValue}"/>.</returns>
    public new bool Remove(TKey key)
    {
        if (!base.Remove(key)) return false;
        
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, key));
        
        OnPropertyChanged(nameof(Count));
        OnPropertyChanged(nameof(Keys));
        OnPropertyChanged(nameof(Values));
        
        return true;
    }

    /// <summary>
    /// Gets or sets the value associated with the specified key.
    /// Hide the base indexer to raise the <see cref="CollectionChanged"/> event
    /// </summary>
    /// <param name="key">The key of the value to get or set.</param>
    /// <returns>The value associated with the specified key. If the specified key is not found, a get operation throws a <see cref="KeyNotFoundException"/>,
    /// and a set operation creates a new element with the specified key.</returns>
    /// <exception cref="KeyNotFoundException">The property is retrieved and key does not exist in the collection.</exception>
    public new TValue this[TKey key]
    {
        get => base[key];
        set
        {
            
            var isValue = TryGetValue(key, out var oldValue);
            base[key] = value;

            if (isValue)
            {
                OnCollectionChanged(ReplaceCollectionEventArgs(key, value, oldValue!));
                OnPropertyChanged(nameof(Values));
            } 
            else
            {
                OnCollectionChanged(AddCollectionEventArgs(key, value));
                OnPropertyChanged(nameof(Count));
                OnPropertyChanged(nameof(Keys));
                OnPropertyChanged(nameof(Values));
            }
        }
    }
}
