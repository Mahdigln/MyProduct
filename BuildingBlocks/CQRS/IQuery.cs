using ErrorOr;
using MediatR;

namespace BuildingBlocks.CQRS;

public interface IQuery<TResponse> : IRequest<ErrorOr<TResponse>>
where TResponse : notnull
{

}