using BuildingBlocks.CQRS;
using Domain.IRepository;
using ErrorOr;

namespace Application.Services.Product.Command.UpdateProduct;

public sealed class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
{
    private readonly IProductRepository _productRepository;

    public UpdateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<ErrorOr<UpdateProductCommandResponse>> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
    {

        var product = await _productRepository.FirstOrDefaultAsync(p =>
            p.Id == request.ProductId
            && p.UserId == request.UserId
            , cancellationToken);
        if (product is null)
            return Error.Failure("productIsNull", "محصول وجود ندارد یا متعلق به شما نیست");


        product.Name = request.Name ?? product.Name;
        product.ProduceDate = request.ProduceDate;
        product.ManufacturePhone = request.ManufacturePhone ?? product.ManufacturePhone;
        product.ManufactureEmail = request.ManufactureEmail ?? product.ManufactureEmail;
        product.IsAvailable = request.IsAvailable;

        _productRepository.Update(product);
        await _productRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return new UpdateProductCommandResponse();
    }
}
