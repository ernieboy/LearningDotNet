namespace LearningDotNet.Domain.Entities;

public class BaseEntity
{
    public Guid Id { get; private set; }

    public DateTime DateCreated { get; private set; }

    public DateTime DateLastUpdated { get; private set; }
}
