using LearningDotNet.Infrastructure.EntityFramework.Interceptors;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using LearningDotNet.Domain.Entities;

namespace LearningDotNet.Infrastructure.EntityFramework;

public class LearningDotNetContext : DbContext
{
    public LearningDotNetContext(DbContextOptions<LearningDotNetContext> options)
        : base(options)
    { }

    public DbSet<Student> Students { get; set; }

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