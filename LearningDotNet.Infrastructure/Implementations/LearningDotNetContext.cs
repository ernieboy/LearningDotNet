using LearningDotNet.Application.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace LearningDotNet.Infrastructure.Implementations
{
    public class LearningDotNetContext : DbContext, ILearningDotNetContext
    {
    }
}
