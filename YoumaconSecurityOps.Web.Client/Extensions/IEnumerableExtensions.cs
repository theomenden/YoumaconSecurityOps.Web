using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Dynamic.Core;

namespace YoumaconSecurityOps.Web.Client.Extensions;

    public static class IEnumerableExtensions
    {
        public static bool IsOrdered<T>(this IQueryable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Expression.Type == typeof(IOrderedQueryable);
        }

        /// <summary>
        /// Orders the provided <see cref="IQueryable{T}"/> <paramref name="source"/> by a single <see cref="ColumnState"/> provided by <paramref name="columnState"/>
        /// </summary>
        /// <typeparam name="T">The underlying type</typeparam>
        /// <param name="source">Provided list of entities of <typeparamref name="T"/></param>
        /// <param name="columnState">The provided column, and direction for sorting</param>
        /// <returns>An <see cref="IEnumerable{T}"/> that is ordered by the provided <see cref="ColumnState"/></returns>
        public static IEnumerable<T> DynamicSort<T>(this IEnumerable<T> source, ColumnState columnState)
        {
            return SortBy(source.AsQueryable(), columnState).ToList();
        }

        /// <summary>
        /// Orders the provided <see cref="IQueryable{T}"/> <paramref name="source"/> by the columns in <paramref name="columnStates"/>
        /// </summary>
        /// <typeparam name="T">The underlying type</typeparam>
        /// <param name="source">Provided list of entities of <typeparamref name="T"/></param>
        /// <param name="columnStates">The provided list of columns and their sort directions as a <see cref="ColumnState"/></param>
        /// <returns>An <see cref="IEnumerable{T}"/> that is ordered by the provided <paramref name="columnStates"/></returns>
        public static IEnumerable<T> DynamicSort<T>(this IEnumerable<T> source, IEnumerable<ColumnState> columnStates)
        {
            source = columnStates
                .Where(cs => cs.SortDirection is not SortDirection.Default)
                .Aggregate(source, (current, columnState) => SortBy(current.AsQueryable(), columnState));

            return source.ToList();
        }

        public static IEnumerable<T> DynamicFilter<T>(this IEnumerable<T> source, IEnumerable<ColumnState> columnStates)
        {
            source = columnStates
                .Where(cs => !String.IsNullOrWhiteSpace(cs.SearchValue))
                .Aggregate(source, (current, columnState) => FilterBy(current.AsQueryable(), columnState));

            return source.ToList();
        }

        public static IEnumerable<IEnumerable<T>> Paging<T>(this IEnumerable<T> source, DataGridReadDataEventArgs<T> columnInfo)
        {
            return source.Chunk(columnInfo.PageSize).ToList();
        }

        private static IQueryable<T> SortBy<T>(this IQueryable<T> source, ColumnState columnState)
        {
        if (!source.IsOrdered())
        {
            return source.OrderBy($"{columnState.Field} {columnState.GetSortDirection()}");
        }

        var orderedQuery = source as IOrderedQueryable<T>;

        return orderedQuery?.ThenBy($"{columnState.Field} {columnState.GetSortDirection()}");
    }

        private static IQueryable<T> FilterBy<T>(this IQueryable<T> source, ColumnState columnState)
        {
            source = source.WhereInterpolated($"{columnState.Field} == {columnState.SearchValue}");

            return source;
        }
    }

