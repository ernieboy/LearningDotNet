using FluentValidation;
using MediatR;

namespace LearningDotNet.Application.Features.Students;

public class CreateStudentRequest : IRequest<CreateStudentResponse>
{
    public string Firstname { get; set; } = null!;

    public string Surname { get; set; } = null!;
}
public class CreateStudentResponse
{
    public bool Success { get; set; }
}

public class CreateStudentRequestHandler : IRequestHandler<CreateStudentRequest, CreateStudentResponse>
{
    public Task<CreateStudentResponse> Handle(CreateStudentRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

public class CreateStudentRequestValidator : AbstractValidator<CreateStudentRequest>
{
    public CreateStudentRequestValidator()
    {
        RuleFor(p => p.Firstname).NotEmpty().MaximumLength(50);
        RuleFor(p => p.Surname).NotEmpty().MaximumLength(50);
    }
}