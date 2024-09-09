using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configurations;
public static class ConfigurationMappings
{
    public static void AddCustomIdentityMappings(this ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ConfigurationMappings).Assembly);
    }
}
