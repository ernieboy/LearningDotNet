using System.ComponentModel.DataAnnotations;

namespace LearningDotNet.Domain.Entities;

public class BaseEntity
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public DateTimeOffset DateCreatedUtc { get; private set; }

    public DateTimeOffset DateLastUpdatedUtc { get; private set; }

    [Timestamp]
    public byte[]? RowVersion { get; private set; }

    public void AddEntityCreationAuditData(DateTimeOffset dateTimeOffset)
    {
        DateCreatedUtc = dateTimeOffset;
        RowVersion = Guid.NewGuid().ToByteArray();
    }

    public void AddEntityUpdateAuditData(DateTimeOffset dateTimeOffset)
    {
        DateLastUpdatedUtc = dateTimeOffset;
        RowVersion = Guid.NewGuid().ToByteArray();
    }
}
