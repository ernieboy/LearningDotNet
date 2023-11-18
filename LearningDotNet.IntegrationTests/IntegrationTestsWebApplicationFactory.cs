using LearningDotNet.Infrastructure.Implementations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Respawn;
using System.Data.Common;
using Testcontainers.MsSql;
using Xunit;

namespace LearningDotNet.Api.IntegrationTests;

public class IntegrationTestsWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlContainer? _msSqlContainer = new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2019-CU18-ubuntu-20.04")
            .Build();
    private DbConnection _dbConnection = default!;
    private Respawner _respawner = default!;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var dbContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<LearningDotNetContext>));

            if (dbContextDescriptor is not null)
            {
                services.Remove(dbContextDescriptor);
            }

            services.AddDbContext<LearningDotNetContext>(options =>
            {
                var connectionString = _msSqlContainer?.GetConnectionString()!;
                //Now point our application is pointing to a real SQL Server instance running in Docker container
                options.UseSqlServer(connectionString);
            });
            using var scope = services.BuildServiceProvider().CreateScope();
        });
    }

    public async Task ResetDatabase()
    {
        await _respawner.ResetAsync(_dbConnection);
    }

    public async Task InitializeAsync()
    {
        await _msSqlContainer!.StartAsync();
        await InitialiseRespawner();
    }

    private async Task InitialiseRespawner()
    {
        _dbConnection = new SqlConnection(_msSqlContainer!.GetConnectionString());
        await _dbConnection.OpenAsync();

        _respawner = await Respawner.CreateAsync(_dbConnection, new RespawnerOptions
        {
            DbAdapter = DbAdapter.SqlServer
        });
    }

    Task IAsyncLifetime.DisposeAsync()
    {
        return _msSqlContainer!.DisposeAsync().AsTask();
    }
}