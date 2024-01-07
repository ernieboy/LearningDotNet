using LearningDotNet.Domain.Entities;

namespace LearningDotNet.Domain.Interfaces;

public interface IStudentRepository
{
    void Add(Student student);

    Task<Student?> FindById(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<Student?>> FindAll(CancellationToken cancellationToken);

    void Update(Student student);

    void Delete(Student student);
}