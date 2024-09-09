using BuildingBlocks.CQRS;

namespace Application.Services.Product.Command.DeleteProduct;

public sealed class DeleteProductCommandRequest : ICommand<DeleteProductCommandResponse>
{
    public int ProductId { get; set; }
    public int UserId { get; set; }
}

public sealed class DeleteProductCommandResponse
{
}