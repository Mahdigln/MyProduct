using BuildingBlocks.CQRS;
using Domain.IRepository;
using ErrorOr;

namespace Application.Services.Product.Command.DeleteProduct;

public sealed class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommandRequest, DeleteProductCommandResponse>
{
    private readonly IProductRepository _productRepository;

    public DeleteProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<ErrorOr<DeleteProductCommandResponse>> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
    {

        var product = await _productRepository.FirstOrDefaultAsync(p =>
            p.Id == request.ProductId
            && p.UserId == request.UserId
            , cancellationToken);
        if (product is null)
            return Error.Failure("productIsNull", "محصول وجود ندارد یا متعلق به شما نیست");


        _productRepository.Delete(product);
        await _productRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return new DeleteProductCommandResponse();
    }
}
