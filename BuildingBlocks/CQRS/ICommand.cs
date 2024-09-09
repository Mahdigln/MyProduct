using ErrorOr;
using MediatR;

namespace BuildingBlocks.CQRS;

public interface ICommand<TResponse> : IRequest<ErrorOr<TResponse>>
    where TResponse : notnull
{

}