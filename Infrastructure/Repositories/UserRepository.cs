using Domain.Entities.Identity;
using Domain.IRepository;
using Infrastructure.Context;

namespace Infrastructure.Repositories;

public sealed class UserRepository : RepositoryBase<User>, IUserRepository
{
    public UserRepository(MainDbContext dbContext) : base(dbContext)
    {
    }
}