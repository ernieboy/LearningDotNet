using FluentValidation;
using LearningDotNet.Common;
using LearningDotNet.Domain.Entities;
using LearningDotNet.Domain.Interfaces;
using MediatR;

namespace LearningDotNet.Application.Features.Students;

public class CreateStudentRequest : IRequest<CreateStudentResponse>
{
    public string Firstname { get; set; } = string.Empty;

    public string Lastname { get; set; } = string.Empty;

    public string DateOfBirth { get; set; } = string.Empty;
}

public class CreateStudentResponse
{
    public bool Success { get; set; }
}

public class CreateStudentRequestHandler(IUnitOfWork unitOfWork,
        IStudentRepository studentRepository)
    : IRequestHandler<CreateStudentRequest, CreateStudentResponse>
{
    public async Task<CreateStudentResponse> Handle(CreateStudentRequest request, CancellationToken cancellationToken)
    {
        var student = new Student(request.Firstname, request.Lastname, DateOnly.Parse(request.DateOfBirth, Constants.UkCultureInfo));
        studentRepository.Add(student);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return new CreateStudentResponse { Success = true };
    }
}

public class CreateStudentRequestValidator : AbstractValidator<CreateStudentRequest>
{
    public CreateStudentRequestValidator()
    {
        RuleFor(p => p.Firstname).NotEmpty().MaximumLength(StudentValidation.FirstNameMaxLength);
        RuleFor(p => p.Lastname).NotEmpty().MaximumLength(StudentValidation.LastNameMaxLength);
        RuleFor(p => p.DateOfBirth).Must(p => FluentValidationsHelper.BeAValidDate(p, Constants.UkCultureInfo))
            .WithMessage(ValidationMessages.UkDateFormatValidationHint);
    }
}