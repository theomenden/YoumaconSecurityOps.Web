using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

namespace YoumaconSecurityOps.Core.Shared.Extensions;

public static class DbSetExtensions
{
    /// <summary>
    /// Checks an existing <paramref name="source"/> for entities that match the given <paramref name="predicate"/> and removes them.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    public static void RemoveIfExists<T>(this DbSet<T> source, Expression<Func<T, bool>> predicate) where T : class
    {
        foreach (var element in source.Where(predicate))
        {
            source.Remove(element);
        }
    }

    /// <summary>
    /// Finds entities in the provided <paramref name="source"/> that mach the given <paramref name="predicate"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <returns><see cref="IEnumerable{T}"/>: <typeparam name="T"></typeparam></returns>
    public static IEnumerable<T> FindAll<T>(this DbSet<T> source, Expression<Func<T, bool>> predicate) where T : class
    {
        foreach (var item in source.Where(predicate))
        {
            yield return item;
        }
    }

    /// <summary>
    /// Retrieves all the members of the underlying <param name="source"></param> in the database that match the given <param name="predicate"></param>
    /// </summary>
    /// <typeparam name="T">The entity we're acting on</typeparam>
    /// <param name="source">The provided <see cref="DbSet{TEntity}"/></param>
    /// <param name="predicate">A list of conditions to search under</param>
    /// <returns>An <see cref="IAsyncEnumerable{T}"/> : <typeparam name="T"></typeparam></returns>
    public static IAsyncEnumerable<T> FindAllAsync<T>(this DbSet<T> source,
        Expression<Func<T, bool>> predicate)
        where T : class
    {
        return source
            .Where(predicate)
            .AsAsyncEnumerable();
    }
}

