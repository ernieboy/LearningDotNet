using LearningDotNet.Domain.Entities;
using LearningDotNet.Domain.Interfaces;
using LearningDotNet.Infrastructure.EntityFramework;

namespace LearningDotNet.Infrastructure.Repositories;

public class StudentRepository(LearningDotNetContext learningDotNetContext) : IStudentRepository
{
    public void Add(Student student)
    {
        learningDotNetContext.Set<Student>().Add(student);
    }
}