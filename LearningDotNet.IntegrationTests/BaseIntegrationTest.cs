using LearningDotNet.Infrastructure.Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace LearningDotNet.Api.IntegrationTests;

[Collection("Test collection")]
public abstract class BaseIntegrationTest : IAsyncLifetime
{
    protected readonly LearningDotNetContext LearningDotNetContext;
    protected readonly IConfiguration Configuration;
    protected readonly HttpClient HttpClient;
    protected readonly Func<Task> ResetDatabase;

    protected BaseIntegrationTest(IntegrationTestsWebApplicationFactory factory)
    {
        HttpClient = factory.CreateClient();
        IServiceScope serviceScope = factory.Services.CreateAsyncScope();
        Configuration = serviceScope.ServiceProvider.GetRequiredService<IConfiguration>();
        LearningDotNetContext = serviceScope.ServiceProvider.GetRequiredService<LearningDotNetContext>();
        ResetDatabase = factory.ResetDatabase;
    }

    public Task DisposeAsync() => ResetDatabase();

    public Task InitializeAsync() => Task.CompletedTask;
}