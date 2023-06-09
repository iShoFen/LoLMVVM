namespace Shared.Mappers;

/// <summary>
/// A generic mapper for Base enums to Other enums
/// </summary>
/// <typeparam name="TBase">The base type</typeparam>
/// <typeparam name="TOther">The other type</typeparam>
internal class EnumsMapper<TBase, TOther> where TBase : Enum 
                                        where TOther : Enum
{
    /// <summary>
    /// The Base to Other mappping
    /// </summary>
    private readonly HashSet<(TBase, TOther)> mapper = new();

    /// <summary>
    /// Get the Base from the Other
    /// </summary>
    /// <param name="other">The Other</param>
    /// <returns>The Base</returns>
    public TBase GetBase(TOther other) => mapper.FirstOrDefault(x => x.Item2.Equals(other)).Item1;

    /// <summary>
    /// Get the Other from the Base
    /// </summary>
    /// <param name="base">The Base</param>
    /// <returns>The Other</returns>
    public TOther GetOther(TBase @base) => mapper.FirstOrDefault(x => x.Item1.Equals(@base)).Item2;
    
    /// <summary>
    /// Constructor for the mapper (add all the enums mapping)
    /// </summary>
    /// <param name="tuples"> The mapping </param>
    public EnumsMapper(params (TBase, TOther)[] tuples) => mapper.UnionWith(tuples);
}
