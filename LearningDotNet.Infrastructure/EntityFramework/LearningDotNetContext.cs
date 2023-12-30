using LearningDotNet.Infrastructure.EntityFramework.Interceptors;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LearningDotNet.Infrastructure.EntityFramework;

public class LearningDotNetContext : DbContext
{
    public LearningDotNetContext(DbContextOptions<LearningDotNetContext> options)
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new EntityAuditInterceptor());
    }
}