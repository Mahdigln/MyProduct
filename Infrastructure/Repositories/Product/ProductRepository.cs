using Domain.IRepository;
using Infrastructure.Context;

namespace Infrastructure.Repositories.Product;

public sealed class ProductRepository : RepositoryBase<Domain.Entities.Product.Product>, IProductRepository
{
    public ProductRepository(MainDbContext dbContext) : base(dbContext)
    {
    }
}