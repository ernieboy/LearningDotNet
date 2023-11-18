using LearningDotNet.Common;

namespace LearningDotNet.Application.Features.StudentManager;

public class Student : BaseEntity
{
    public string Firstname { get; set; } = string.Empty;

    public string Lastname { get; set; } = string.Empty;

    public DateOnly DateOfBirth { get; private set; }
}

