using FluentValidation;
using LearningDotNet.Domain.Interfaces;
using MediatR;

namespace LearningDotNet.Application.Features.Students;

public class DeleteStudentRequest : IRequest<DeleteStudentResponse>
{
    public Guid? Id { get; set; }
}

public class DeleteStudentResponse
{
    public bool Success { get; set; }
}

public class DeleteStudentRequestHandler(IUnitOfWork unitOfWork,
        IStudentRepository studentRepository)
    : IRequestHandler<DeleteStudentRequest, DeleteStudentResponse>
{
    public async Task<DeleteStudentResponse> Handle(DeleteStudentRequest request, CancellationToken cancellationToken)
    {
        var student = await studentRepository.FindById(request.Id!.Value, cancellationToken);
        ArgumentNullException.ThrowIfNull(student);

        studentRepository.Delete(student);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return new DeleteStudentResponse { Success = true };
    }
}

public class DeleteStudentRequestValidator : AbstractValidator<DeleteStudentRequest>
{
    public DeleteStudentRequestValidator()
    {
        RuleFor(p => p.Id).Must(p => p != Guid.Empty)
            .WithMessage(ValidationMessages.InvalidId);
    }
}