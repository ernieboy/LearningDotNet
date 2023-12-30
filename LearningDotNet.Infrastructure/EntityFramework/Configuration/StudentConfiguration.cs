using LearningDotNet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningDotNet.Infrastructure.EntityFramework.Configuration;

internal class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(p => p.Firstname).IsRequired().HasMaxLength(50);
        builder.Property(p => p.Lastname).IsRequired().HasMaxLength(50);
        builder.Property(p => p.DateOfBirth).IsRequired();
        builder.Property(p => p.RowVersion)
            .IsConcurrencyToken();
        builder.ToTable("Students");
    }
}