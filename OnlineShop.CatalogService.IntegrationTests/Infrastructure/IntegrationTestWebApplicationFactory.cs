using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineShop.CatalogService.Infrastructure.Persistence;
using System.Security.Cryptography;
using Testcontainers.PostgreSql;
using Xunit;

namespace OnlineShop.CatalogService.IntegrationTests.Infrastructure;

public class IntegrationTestWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgres = new PostgreSqlBuilder()
        .WithImage("postgres:17")
        .WithDatabase("catalog_test_db")
        .WithUsername("test")
        .WithPassword("test")
        .Build();

    public string ConnectionString => _postgres.GetConnectionString();

    public async Task InitializeAsync()
    {
        await _postgres.StartAsync();

        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();

        await dbContext.Database.MigrateAsync();
    }

    public new async Task DisposeAsync()
    {
        await _postgres.DisposeAsync();
        await base.DisposeAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        //builder.ConfigureAppConfiguration((context, config) =>
        //{
        //    config.AddInMemoryCollection(new Dictionary<string, string?>
        //    {
        //        ["ConnectionStrings:Default"] = _postgres.GetConnectionString()
        //    });           
        //});
        builder.UseSetting("ConnectionStrings:Default", _postgres.GetConnectionString());
    }
}