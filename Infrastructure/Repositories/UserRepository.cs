using Domain.Entities.Identity;
using Domain.IRepository;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class UserRepository : RepositoryBase<User>, IUserRepository
{
    public UserRepository(MainDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<UserProductsInfo>> GetAllProducts(CancellationToken cancellation)
    {
        var userProductsInfos = await Entities
            .AsNoTrackingWithIdentityResolution()
            .OrderBy(c => c.UserName) // Add this line to order by UserName
            .Select(c => new UserProductsInfo
            {
                UserId = c.Id,
                UserName = c.UserName,
                UserProduct = c.Products
                    .Where(product => product != null)
                    .Select(product => new UserProductDto
                    {
                        IsAvailable = product.IsAvailable,
                        Name = product.Name,
                        UserId = product.UserId,
                        DeletedDateTime = product.DeletedDateTime,
                        IsDeleted = product.IsDeleted,
                        ManufactureEmail = product.ManufactureEmail,
                        ManufacturePhone = product.ManufacturePhone,
                        ProduceDate = product.ProduceDate,
                    }).ToList()
            })
            .ToListAsync(cancellation);

        return userProductsInfos;
    }

}