using LearningDotNet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace LearningDotNet.Infrastructure.EntityFramework.Interceptors;

internal sealed class EntityAuditInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        SetEntityAuditData(eventData.Context!);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void SetEntityAuditData(DbContext context)
    {
        foreach (var entry in context.ChangeTracker.Entries())
        {
            if (entry.Entity is not BaseEntity entity) continue;
            var now = DateTimeOffset.UtcNow;

            switch (entry.State)
            {
                case EntityState.Added:
                    entity.SetDateCreatedUtc(now);
                    entity.SetDateLastUpdatedUtc(now);
                    break;
                case EntityState.Modified:
                    entity.SetDateLastUpdatedUtc(now);
                    break;
                case EntityState.Detached:
                    break;
                case EntityState.Unchanged:
                    break;
                case EntityState.Deleted:
                    break;
                default:
                    throw new DbUpdateException(
                        $"{nameof(EntityAuditInterceptor)} encountered unknown entity state while saving entity with Id {entity.Id}.");
            }
        }
    }
}