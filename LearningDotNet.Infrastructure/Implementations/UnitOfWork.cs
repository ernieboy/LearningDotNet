using LearningDotNet.Domain.Interfaces;
using LearningDotNet.Infrastructure.EntityFramework;

namespace LearningDotNet.Infrastructure.Implementations;

public class UnitOfWork(LearningDotNetContext learningDotNetContext) : IUnitOfWork
{
    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return learningDotNetContext.SaveChangesAsync(cancellationToken);
    }
}