using BuildingBlocks.CQRS;
using Domain.Abstractions;
using Domain.IRepository;
using ErrorOr;
using Microsoft.EntityFrameworkCore;


namespace Application.Services.Identity.Query.UserProducts;

public sealed class UserProductsQueryHandler : IQueryHandler<UserProductsQueryRequest, UserProductsQueryResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IProductRepository _productRepository;

    public UserProductsQueryHandler(IUserRepository userRepository, IProductRepository productRepository)
    {
        _userRepository = userRepository;
        _productRepository = productRepository;
    }

    public async Task<ErrorOr<UserProductsQueryResponse>> Handle(UserProductsQueryRequest request,
        CancellationToken cancellationToken)
    {

        var userProducts = await _userRepository.FindWithPagination(
            predicate: x => true,
            include: x => x.Include(c => c.Products),
            pageIndex: request.PageIndex,
            pageSize: request.PageSize,
            selector: x => new UserDto()
            {
                UserId = x.Id,
                UserName = x.UserName,
                UserProduct = x.Products.Select(p => new UserProductDto
                {
                    UserId = x.Id,
                    Name = p.Name,
                    IsAvailable = p.IsAvailable,
                    IsDeleted = p.IsDeleted,
                    ProduceDate = p.ProduceDate,
                    ManufactureEmail = p.ManufactureEmail,
                    ManufacturePhone = p.ManufacturePhone,
                }).ToList()
            },
            orderBy: orderBy => orderBy.Id,
            isDescending: request.SortDirection != SortDirection.Ascending,
            cancellationToken: cancellationToken);


        var response = new UserProductsQueryResponse
        {
            User = userProducts.ToList()
        };
        return response;
    }

}
