namespace YsecOps.Core.Mediator.Requests.Queries;

public record GetTotalRadiosQuery: IRequest<Int32>;

public record GetCheckedOutRadiosCountQuery: IRequest<Int32>;

public record GetChargingRadiosCountQuery : IRequest<Int32>;
