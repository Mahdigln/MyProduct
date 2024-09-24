using BuildingBlocks.DependencyInjection;
using Domain.Abstractions;
using Domain.Entities.Identity;
using Domain.Models;

namespace Domain.IRepository;

public interface IUserRepository : IRepositoryBase<User>, IScopeLifetime
{
    Task<List<UserProductsInfo>> GetAllProducts(CancellationToken cancellation);
}

