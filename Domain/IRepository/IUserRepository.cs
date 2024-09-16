using BuildingBlocks.DependencyInjection;
using Domain.Abstractions;
using Domain.Entities.Identity;

namespace Domain.IRepository;

public interface IUserRepository : IRepositoryBase<User>, IScopeLifetime
{
}