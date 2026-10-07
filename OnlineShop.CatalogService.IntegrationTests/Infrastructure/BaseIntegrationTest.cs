namespace OnlineShop.CatalogService.IntegrationTests.Infrastructure;

[Collection(nameof(DatabaseCollection))]
public class BaseIntegrationTest : IAsyncLifetime
{
    protected readonly IntegrationTestWebApplicationFactory Factory;
    protected readonly HttpClient Client;
    private DataBaseRespawner _respawner = null!;
    protected BaseIntegrationTest(IntegrationTestWebApplicationFactory factory)
    {
        Factory = factory;
        Client = factory.CreateClient();
    }

    public async Task InitializeAsync()
    {
        _respawner = await DataBaseRespawner.CreateAsync(Factory.ConnectionString);
        await _respawner.ResetAsync();
    }

    public async Task DisposeAsync()
    {
        await _respawner.DisposeAsync();
    }
}