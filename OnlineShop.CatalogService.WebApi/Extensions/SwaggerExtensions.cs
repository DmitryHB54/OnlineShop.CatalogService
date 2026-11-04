using Microsoft.OpenApi;

namespace OnlineShop.CatalogService.WebApi.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(config =>
        {
            config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorisation",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter JWT token (without Bearer prefix)"
            });
            config.AddSecurityRequirement(document => new OpenApiSecurityRequirement
            {
                [new OpenApiSecuritySchemeReference("Bearer", document)] = new List<string>()
            });
        });
        return services;
    }

    public static IApplicationBuilder UseSwaggerDocumantation(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("swagger/v1/swagger.json", "Catalog API V1");
                c.RoutePrefix = string.Empty;
            });
        }
        return app;
    }
}