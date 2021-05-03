using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoumaconSecurityOps.Core.Shared.Extensions
{
    public static class IsBetweenExtensions
    {
        /// <summary>
        /// lb &lt;= value &lt;= ub
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="lowerBound"></param>
        /// <param name="upperBound"></param>
        /// <returns>true or false</returns>
        public static bool IsBetween<T>(this T value, T lowerBound, T upperBound) where T : IComparable<T>
        {
            return lowerBound.CompareTo(value) <= 0 && upperBound.CompareTo(value) >= 0;
        }

        /// <summary>
        /// lb &lt; value &lt;= ub
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="lowerBound"></param>
        /// <param name="upperBound"></param>
        /// <returns>true or false</returns>
        public static bool IsBetweenExclusiveLowerBound<T>(this T value, T lowerBound, T upperBound) where T : IComparable<T>
        {
            return (lowerBound.CompareTo(value) < 0) && (value.CompareTo(upperBound) <= 0);
        }

        /// <summary>
        /// lb &lt;= value &lt; ub
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="lowerBound"></param>
        /// <param name="upperBound"></param>
        /// <returns>true or false</returns>
        public static bool IsBetweenExclusiveUpperBound<T>(this T value, T lowerBound, T upperBound) where T : IComparable<T>
        {
            return (lowerBound.CompareTo(value) <= 0) && (upperBound.CompareTo(value) > 0);
        }

        /// <summary>
        /// lb &lt; value &lt; ub
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="lowerBound"></param>
        /// <param name="upperBound"></param>
        /// <returns>true or false</returns>
        public static bool IsBetweenExclusiveBounds<T>(this T value, T lowerBound, T upperBound) where T : IComparable<T>
        {
            return (lowerBound.CompareTo(value) < 0) && (upperBound.CompareTo(value) > 0);
        }

        /// <summary>
        /// value &lt; lb or ub &lt; value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="lowerBound"></param>
        /// <param name="upperBound"></param>
        /// <returns>true or false</returns>
        public static bool IsNotBetween<T>(this T value, T lowerBound, T upperBound) where T : IComparable<T>
        {
            return !IsBetween(value, lowerBound, upperBound);
        }


    }
}
