using LearningDotNet.Application;
using LearningDotNet.Domain.Interfaces;
using LearningDotNet.Infrastructure.EntityFramework;
using LearningDotNet.Infrastructure.Implementations;
using LearningDotNet.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace LearningDotNet.Infrastructure.DependencyInjection;

public static class CompositionRoot
{
    public static void RegisterServices(this IServiceCollection serviceCollection, WebApplicationBuilder webApplicationBuilder)
    {
        serviceCollection.AddTransient<IUnitOfWork,UnitOfWork>();
        serviceCollection.AddTransient<IStudentRepository, StudentRepository>();
    

        serviceCollection.Configure<LearningDotNetSettings>(
            webApplicationBuilder.Configuration.GetSection(nameof(LearningDotNetSettings)));

        using var scope = serviceCollection.BuildServiceProvider().CreateScope();
        var settingsOptions = scope.ServiceProvider.GetRequiredService<IOptionsMonitor<LearningDotNetSettings>>();
        var settings = settingsOptions.CurrentValue;

        serviceCollection.AddDbContext<LearningDotNetContext>(
        options => options.UseSqlServer(settings.DatabaseConnectionString));

      
    }
}