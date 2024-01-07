namespace LearningDotNet.Domain.Entities;

public class Student : BaseEntity
{
    internal Student()
    {
    }
    public Student(string firstName, string lastName, DateOnly dateOfBirth)
    {
        Firstname = firstName;
        Lastname = lastName;
        DateOfBirth = dateOfBirth;
    }

    public string Firstname { get; private set; } = string.Empty;

    public string Lastname { get; private set; } = string.Empty;

    public DateOnly DateOfBirth { get; private set; }

    public void Update(string firstName, string lastName, DateOnly dateOfBirth)
    {
        ArgumentException.ThrowIfNullOrEmpty(firstName);
        ArgumentException.ThrowIfNullOrEmpty(lastName);

        Firstname = firstName; 
        Lastname = lastName; 
        DateOfBirth = dateOfBirth;
    }
}
