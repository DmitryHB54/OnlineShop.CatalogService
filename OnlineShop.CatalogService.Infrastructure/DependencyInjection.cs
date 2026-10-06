using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineShop.CatalogService.Application.Common.Interfaces;
using OnlineShop.CatalogService.Infrastructure.Persistence;
using OnlineShop.CatalogService.Infrastructure.Persistence.Repositories;

namespace OnlineShop.CatalogService.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default")
            ?? throw new InvalidOperationException("Connection string 'Default' is missing");

        services.AddDbContext<CatalogDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}