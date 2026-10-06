using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using OnlineShop.CatalogService.Application.Common.Behaviors;
using System.Reflection;

namespace OnlineShop.CatalogService.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssembly(assembly);

        services.AddAutoMapper(cfg => { }, assembly);

        return services;
    }
}