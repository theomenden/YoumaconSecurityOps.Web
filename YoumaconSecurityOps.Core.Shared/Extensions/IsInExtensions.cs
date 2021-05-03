using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoumaconSecurityOps.Core.Shared.Extensions
{
    public static class IsInExtensions
    {
        /// <summary>
        /// Checks if value is in collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="valueToCheck"></param>
        /// <param name="sourceToCheckAgainst"></param>
        /// <returns>true if value is found in collection, false if value is not found</returns>
        public static bool IsIn<T>(this T valueToCheck, IEnumerable<T> sourceToCheckAgainst)
        {
            return sourceToCheckAgainst.Contains(valueToCheck);
        }

        /// <summary>
        /// Checks if value is not in collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="valueToCheck"></param>
        /// <param name="sourceToCheckAgainst"></param>
        /// <returns>true if value is not found in collection, false if value is found</returns>
        public static bool IsNotIn<T>(this T valueToCheck, IEnumerable<T> sourceToCheckAgainst)
        {
            return !IsIn(valueToCheck, sourceToCheckAgainst);
        }
    }
}
