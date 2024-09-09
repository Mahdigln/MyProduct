using BuildingBlocks.CQRS;
using Domain.IRepository;
using ErrorOr;
using Mapster;

namespace Application.Services.Product.Command.AddProduct;

public sealed class AddProductCommandHandler : ICommandHandler<AddProductCommandRequest, AddProductCommandResponse>
{
    private readonly IProductRepository _productRepository;

    public AddProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<ErrorOr<AddProductCommandResponse>> Handle(AddProductCommandRequest request, CancellationToken cancellationToken)
    {

        var isDuplicate = await _productRepository
            .AnyAsync(p => p.ManufactureEmail == request.ManufactureEmail &&
                           p.ProduceDate == request.ProduceDate, cancellationToken);
        if (isDuplicate)
            return Error.Failure("isDuplicate", "محصول با این ایمیل تولیدکننده و تاریخ تولید قبلا ثبت شده است");

        var product = request.Adapt<Domain.Entities.Product.Product>();
        product.UserId = request.UserId;

        await _productRepository.AddAsync(product, cancellationToken);
        await _productRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return new AddProductCommandResponse();
    }
}