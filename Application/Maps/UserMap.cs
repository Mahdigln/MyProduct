using Application.Services.Identity.Query.UserProducts;
using Domain.Entities.Identity;
using Mapster;

namespace Application.Maps;
public sealed class UserMap : IRegister
{
    public void Register(TypeAdapterConfig config)
    {

        TypeAdapterConfig<User, UserDto>
            .NewConfig()
            .Map(dest => dest.UserId, src => src.Id)
            .Map(dest => dest.UserProduct, src => src.Products.Adapt<IReadOnlyList<UserProductDto>>());
    }
}

