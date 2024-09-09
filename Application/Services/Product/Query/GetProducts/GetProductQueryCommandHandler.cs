using BuildingBlocks.CQRS;
using Domain.Abstractions;
using Domain.IRepository;
using ErrorOr;

namespace Application.Services.Product.Query.GetProducts;

public sealed class GetProductQueryCommandHandler : IQueryHandler<GetProductQueryRequest, GetProductQueryResponse>
{
    private readonly IProductRepository _productRepository;

    public GetProductQueryCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<ErrorOr<GetProductQueryResponse>> Handle(GetProductQueryRequest request, CancellationToken cancellationToken)
    {
        var result = await _productRepository.FindWithPagination(
            predicate: x =>
                (request.UserId == null || request.UserId == x.UserId
                ),
            include: null,
            pageIndex: request.PageIndex,
            pageSize: request.PageSize,
            selector: x => new ProductListDto()
            {
                Id = x.Id,
                ProduceDate = x.ProduceDate,
                IsAvailable = x.IsAvailable,
                ManufactureEmail = x.ManufactureEmail,
                ManufacturePhone = x.ManufacturePhone,
                Name = x.Name,
            },
            orderBy => orderBy.Id
            ,
            isDescending: request.SortDirection != SortDirection.Ascending,
            cancellationToken: cancellationToken);

        return new GetProductQueryResponse() { Items = result };
    }
}