using LearningDotNet.Common;

namespace LearningDotNet.Application.Features.Students;

public class Student : BaseEntity
{
    public string Firstname { get; private set; } = string.Empty;

    public string Lastname { get; private set; } = string.Empty;

    public DateOnly DateOfBirth { get; private set; }
}
