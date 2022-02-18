namespace YoumaconSecurityOps.Core.Shared.Extensions;

public static class IsInExtensions
{
    /// <summary>
    /// Checks if <paramref name="valueToCheck"/> is in the <paramref name="sourceToCheckAgainst"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="valueToCheck"></param>
    /// <param name="sourceToCheckAgainst"></param>
    /// <returns><seealso cref="bool"/></returns>
    public static bool IsIn<T>(this T valueToCheck, IEnumerable<T> sourceToCheckAgainst)
    {
        return sourceToCheckAgainst.Contains(valueToCheck);
    }

    /// <summary>
    /// Checks if <paramref name="valueToCheck"/> is not in the <paramref name="sourceToCheckAgainst"/>
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