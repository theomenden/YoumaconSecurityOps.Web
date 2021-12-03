using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace YsecItAuthApp.Web.EntityFramework.Extensions
{
    internal static class DbSetExtensions
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
    }
}
