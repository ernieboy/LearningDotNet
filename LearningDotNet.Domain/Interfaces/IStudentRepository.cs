using LearningDotNet.Domain.Entities;

namespace LearningDotNet.Domain.Interfaces;

public interface IStudentRepository
{
    void Add(Student student);
}