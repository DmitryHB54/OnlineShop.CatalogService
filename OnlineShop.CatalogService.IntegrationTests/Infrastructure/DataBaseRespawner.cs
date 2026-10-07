using Npgsql;
using Respawn;

namespace OnlineShop.CatalogService.IntegrationTests.Infrastructure;
public class DataBaseRespawner : IAsyncDisposable
{
    private readonly Respawner _respawner;
    private readonly NpgsqlConnection _connection;

    private DataBaseRespawner(Respawner respawner, NpgsqlConnection connection)
    {
        _respawner = respawner;
        _connection = connection;
    }

    public static async Task<DataBaseRespawner> CreateAsync(string connectionString) 
    {
        var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync();

        var respawner = await Respawner.CreateAsync(connection, new RespawnerOptions
        {
            DbAdapter = DbAdapter.Postgres,
            SchemasToInclude = new[] { "public" }
        });

        return new DataBaseRespawner(respawner, connection);
    }

    public async Task ResetAsync()
    {
        await _respawner.ResetAsync(_connection);
    }

    public async ValueTask DisposeAsync()
    {
        await _connection.DisposeAsync();
    }
}