using BuildingBlocks.CQRS;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.DependencyInjection;
public static class ServiceCollectionConfigurationHandler
{
    public static IServiceCollection RegisterMapping(this IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        TypeAdapterConfig.GlobalSettings.Scan(assemblies);

        return services;
    }

    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {

        var assemblys = AppDomain.CurrentDomain.GetAssemblies();


        services.Scan(selector => selector
            .FromAssemblies(assemblys)
            .AddClasses(filter => filter.AssignableTo(typeof(ICommandHandler<,>)))
            .AsImplementedInterfaces()
            .WithTransientLifetime());

        services.Scan(selector => selector
            .FromAssemblies(assemblys)
            .AddClasses(filter => filter.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithTransientLifetime());

        services.Scan(selector => selector
            .FromAssemblies(assemblys)
            .AddClasses(filter => filter.AssignableTo<IScopeLifetime>())
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.Scan(selector => selector
            .FromAssemblies(assemblys)
            .AddClasses(filter => filter.AssignableTo<ISingletonLifetime>())
            .AsImplementedInterfaces()
            .WithSingletonLifetime());

        services.Scan(selector => selector
            .FromAssemblies(assemblys)
            .AddClasses(filter => filter.AssignableTo<ITransientLifetime>())
            .AsImplementedInterfaces()
            .WithTransientLifetime());

        return services;
    }
}
