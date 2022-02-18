namespace YoumaconSecurityOps.Core.Mediatr.Queries;

/// <summary>
/// <para>Allows for <see cref="IncidentQueryStringParameters"/> to be used when searching for incidents</para>
/// </summary>
/// <remarks><inheritdoc cref="StreamQueryBase{T}"/></remarks>
/// <param name="Parameters">Parameters to be used in the query</param>
public record GetIncidentsWithParametersQuery(IncidentQueryStringParameters Parameters) : StreamQueryBase<IncidentReader>
{
}