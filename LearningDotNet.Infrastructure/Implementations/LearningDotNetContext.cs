using Microsoft.EntityFrameworkCore;

namespace LearningDotNet.Infrastructure.Implementations;

public class LearningDotNetContext : DbContext
{
    public LearningDotNetContext(DbContextOptions<LearningDotNetContext> options)
        :base(options)
    {
        
    }
}

