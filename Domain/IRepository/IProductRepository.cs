using BuildingBlocks.DependencyInjection;
using Domain.Abstractions;
using Domain.Entities.Product;

namespace Domain.IRepository;

public interface IProductRepository : IRepositoryBase<Product>, IScopeLifetime
{
}