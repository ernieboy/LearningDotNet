namespace LearningDotNet.Domain.Entities;

public class BaseEntity
{
    public BaseEntity()
    {
        Id = Guid.NewGuid();
    }
    public Guid Id { get; private set; }

    public DateTimeOffset DateCreatedUtc { get; private set; }

    public DateTimeOffset DateLastUpdatedUtc { get; private set; }

    public void SetDateCreatedUtc(DateTimeOffset dateTimeOffset)
    {
        DateCreatedUtc = dateTimeOffset;
    }

    public void SetDateLastUpdatedUtc(DateTimeOffset dateTimeOffset)
    {
        DateLastUpdatedUtc = dateTimeOffset;
    }
}
