using YSecOps.Data.EfCore.Models;
namespace YsecOps.Core.Mediator.Requests.Queries;
public record GetPronounsQuery : IRequest<List<Pronoun>>;
