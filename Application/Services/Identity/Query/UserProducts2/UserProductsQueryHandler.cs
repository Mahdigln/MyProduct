using BuildingBlocks.CQRS;
using Domain.IRepository;
using ErrorOr;
using Mapster;

namespace Application.Services.Identity.Query.UserProducts2;

public sealed class UserProductsQueryHandler2 : IQueryHandler<UserProductsQueryRequest2, UserProductsQueryResponse2>
{
    private readonly IUserRepository _userRepository;
    private readonly IProductRepository _productRepository;

    public UserProductsQueryHandler2(IUserRepository userRepository, IProductRepository productRepository)
    {
        _userRepository = userRepository;
        _productRepository = productRepository;
    }

    public async Task<ErrorOr<UserProductsQueryResponse2>> Handle(UserProductsQueryRequest2 request,
        CancellationToken cancellationToken)
    {

        var userProductsInfo = await _userRepository.GetAllProducts(cancellationToken);

        if (userProductsInfo == null)
        {
            return Error.Failure("NotFound", "No products found for the user.");
        }

        var userDto = userProductsInfo.Select(u => new UserDto2
        {
            UserId = u.UserId,
            UserName = u.UserName,
            UserProduct = u.UserProduct.Select(p => new UserProductDto2
            {
                Name = p.Name,
                ProduceDate = p.ProduceDate,
                ManufacturePhone = p.ManufacturePhone,
                ManufactureEmail = p.ManufactureEmail,
                IsAvailable = p.IsAvailable,
                UserId = u.UserId,
                IsDeleted = p.IsDeleted,
                DeletedDateTime = p.DeletedDateTime
            }).ToList()
        });

        // var response = new UserProductsQueryResponse2 { User = userDto.ToList() };

        var result = userProductsInfo.Adapt<List<UserDto2>>();
        var response = new UserProductsQueryResponse2 { User = result };

        return response;
    }

}
