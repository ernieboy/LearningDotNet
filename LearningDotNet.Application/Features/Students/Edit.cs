using FluentValidation;
using LearningDotNet.Common;
using LearningDotNet.Domain.Interfaces;
using MediatR;

namespace LearningDotNet.Application.Features.Students;

public class EditStudentRequest : IRequest<EditStudentResponse>
{
    public Guid Id { get; set; }

    public string Firstname { get; set; } = string.Empty;

    public string Lastname { get; set; } = string.Empty;

    public string DateOfBirth { get; set; } = string.Empty;
}

public class EditStudentResponse
{
    public bool Success { get; set; }
}

public class EditStudentRequestHandler(IUnitOfWork unitOfWork,
        IStudentRepository studentRepository)
    : IRequestHandler<EditStudentRequest, EditStudentResponse>
{
    public async Task<EditStudentResponse> Handle(EditStudentRequest request, CancellationToken cancellationToken)
    {
        var student = await studentRepository.FindById(request.Id, cancellationToken);
        ArgumentNullException.ThrowIfNull(student);
        student.Update(request.Firstname, request.Lastname,
            DateOnly.Parse(request.DateOfBirth, Constants.UkCultureInfo));

        studentRepository.Update(student);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return new EditStudentResponse { Success = true };
    }
}

public class EditStudentRequestValidator : AbstractValidator<EditStudentRequest>
{
    public EditStudentRequestValidator()
    {
        RuleFor(p => p.Firstname).NotEmpty().MaximumLength(StudentValidation.FirstNameMaxLength);
        RuleFor(p => p.Lastname).NotEmpty().MaximumLength(StudentValidation.LastNameMaxLength);
        RuleFor(p => p.DateOfBirth).Must(p => FluentValidationsHelper.BeAValidDate(p, Constants.UkCultureInfo))
            .WithMessage(ValidationMessages.UkDateFormatValidationHint);
    }
}