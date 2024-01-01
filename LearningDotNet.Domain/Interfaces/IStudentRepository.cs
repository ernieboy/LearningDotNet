using LearningDotNet.Domain.Entities;

namespace LearningDotNet.Domain.Interfaces;

public interface IStudentRepository
{
    void Add(Student student);

    Task<Student?> FindById(Guid id, CancellationToken cancellationToken);

    void Update(Student student);
}