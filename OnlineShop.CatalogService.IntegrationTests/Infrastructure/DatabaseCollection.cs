using Xunit;

namespace OnlineShop.CatalogService.IntegrationTests.Infrastructure;

[CollectionDefinition(nameof(DatabaseCollection))]
public class DatabaseCollection : ICollectionFixture<IntegrationTestWebApplicationFactory> { }