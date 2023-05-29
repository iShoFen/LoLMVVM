namespace MVVMToolkit;

/// <summary>
/// A generic version of <see cref="ObservableObject"/> That define a <see cref="Model"/> property
/// </summary>
/// <typeparam name="T">The type of the model</typeparam>
public abstract class ObservableObject<T> : ObservableObject
{
    /// <summary>
    /// The model
    /// </summary>
    protected T Model { get; }
    
    /// <summary>
    /// Create a new instance of <see cref="ObservableObject{T}"/>
    /// </summary>
    /// <param name="model"></param>
    protected ObservableObject(T model)
    {
        Model = model;
    }
}