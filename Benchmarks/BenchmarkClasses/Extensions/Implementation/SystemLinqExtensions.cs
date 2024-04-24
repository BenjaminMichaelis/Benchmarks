namespace Benchmarks.BenchmarkClasses.Extensions.Implementation;

/// <summary>
/// Various Linq extensions
/// </summary>
public static class SystemLinqExtensions
{
    /// <summary>
    /// Filters a sequence of values to only those that are not null.
    /// </summary>
    /// <typeparam name="T">The type of the elements of source.</typeparam>
    /// <param name="source">A <see cref="IEnumerable{T}"/> to filter.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> that contains elements from the input that are not null.</returns>
    public static IEnumerable<T> WhereNotNullWithLinqWhereThenSelect<T>(this IEnumerable<T?> source) where T : class?
    {
        return source.Where(item => item is not null).Select(item => item!);
    }

    /// <summary>
    /// Filters a sequence of values to only those that are not null.
    /// </summary>
    /// <typeparam name="T">The type of the elements of source.</typeparam>
    /// <param name="source">A <see cref="IEnumerable{T}"/> to filter.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> that contains elements from the input that are not null.</returns>
    public static IEnumerable<T> WhereNotNullWithLinqWhereThenSelectCast<T>(this IEnumerable<T?> source) where T : class?
    {
        return (IEnumerable<T>)source.Where(item => item is not null);
    }

    /// <summary>
    /// Filters a sequence of values to only those that are not null.
    /// This is functionally equivalent to WhereNotNullWithLinqWhereThenSelectCast
    /// </summary>
    /// <typeparam name="T">The type of the elements of source.</typeparam>
    /// <param name="source">A <see cref="IEnumerable{T}"/> to filter.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> that contains elements from the input that are not null.</returns>
    public static IEnumerable<T> WhereNotNullWithLinqQueryExpression<T>(this IEnumerable<T?> source) where T : class?
    {
        return from item in source
               where item is not null
               select item;
    }

    /// <summary>
    /// Filters a sequence of values to only those that are not null.
    /// </summary>
    /// <typeparam name="T">The type of the elements of source.</typeparam>
    /// <param name="source">A <see cref="IEnumerable{T}"/> to filter.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> that contains elements from the input that are not null.</returns>
    public static IEnumerable<T> WhereNotNullWithIfCheck<T>(this IEnumerable<T?> source) where T : class?
    {
        foreach (var item in source)
        {
            if (item is not null) yield return item;
        }
    }

    /// <summary>
    /// Filters a sequence of values to only those that are not null.
    /// </summary>
    /// <typeparam name="T">The type of the elements of source.</typeparam>
    /// <param name="source">A <see cref="IEnumerable{T}"/> to filter.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> that contains elements from the input that are not null.</returns>
    public static IEnumerable<T> WhereNotNullWithLinqOfType<T>(this IEnumerable<T?> source) where T : class?
    {
        return source.OfType<T>();
    }
}
