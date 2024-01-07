﻿using LearningDotNet.Domain.Entities;
using LearningDotNet.Domain.Interfaces;
using LearningDotNet.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace LearningDotNet.Infrastructure.Repositories;

public class StudentRepository(LearningDotNetContext learningDotNetContext) : IStudentRepository
{
    public void Add(Student student)
    {
        learningDotNetContext.Set<Student>().Add(student);
    }

    public void Update(Student student)
    {
        learningDotNetContext.Attach(student);
        learningDotNetContext.Entry(student).State = EntityState.Modified;
    }

    public void Delete(Student student)
    {
        learningDotNetContext.Attach(student);
        learningDotNetContext.Entry(student).State = EntityState.Deleted;
    }

    public async Task<Student?> FindById(Guid id, CancellationToken cancellationToken = default)
    {
        return await learningDotNetContext.Students.FindAsync(id, cancellationToken);
    }
}