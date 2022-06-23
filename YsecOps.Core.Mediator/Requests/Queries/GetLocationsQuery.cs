using YSecOps.Data.EfCore.Models;
namespace YsecOps.Core.Mediator.Requests.Queries;
public record GetLocationsQuery : IRequest<List<Location>>;
