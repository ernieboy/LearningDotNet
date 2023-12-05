using LearningDotNet.Application;
using LearningDotNet.Infrastructure.Implementations;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace LearningDotNet.Infrastructure.DependencyInjection;

public static class CompositionRoot
{
    public static void RegisterServices(this IServiceCollection serviceCollection, WebApplicationBuilder webApplicationBuilder)
    {
         serviceCollection.Configure<LearningDotNetSettings>(
            webApplicationBuilder.Configuration.GetSection(nameof(LearningDotNetSettings)));

        using var scope = serviceCollection.BuildServiceProvider().CreateScope();
        var settingsOptions = scope.ServiceProvider.GetRequiredService<IOptionsMonitor<LearningDotNetSettings>>();
        var settings = settingsOptions.CurrentValue;


        serviceCollection.AddDbContext<LearningDotNetContext>(
        options => options.UseSqlServer(settings.DatabaseConnectionString));
    }
}