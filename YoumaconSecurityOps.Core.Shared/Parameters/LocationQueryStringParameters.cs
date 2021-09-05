namespace YoumaconSecurityOps.Core.Shared.Parameters
{
    /// <summary>
    /// <para>For use with Location Api requests</para>
    /// <inheritdoc cref="QueryStringParameters"/>
    /// </summary>
    public record LocationQueryStringParameters(string Name, bool IsHotel) : QueryStringParameters;
}
