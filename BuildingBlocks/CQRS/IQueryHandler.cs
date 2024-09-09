using ErrorOr;
using MediatR;

namespace BuildingBlocks.CQRS;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, ErrorOr<TResponse>>
    where TQuery : IQuery<TResponse>
    where TResponse : notnull
{

}