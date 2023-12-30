namespace LearningDotNet.Domain.Entities;

public class Student : BaseEntity
{
    public Student()
    {
        
    }
    public Student(string firstName, string lastName, DateOnly dateOfBirth)
    {
        Firstname = firstName;
        Lastname = lastName;
        DateOfBirth = dateOfBirth;
    }

    public string Firstname { get; set; }

    public string Lastname { get; set; }

    public DateOnly DateOfBirth { get; set; }
}
