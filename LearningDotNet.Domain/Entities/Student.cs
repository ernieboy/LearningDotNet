namespace LearningDotNet.Domain.Entities;

public class Student : BaseEntity
{
    public string Firstname { get; private set; } = string.Empty;

    public string Lastname { get; private set; } = string.Empty;

    public DateOnly DateOfBirth { get; private set; }
}
