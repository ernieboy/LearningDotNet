using FluentValidation;
using LearningDotNet.Domain.Interfaces;
using MediatR;

namespace LearningDotNet.Application.Features.Students;

public class ListStudentByIdRequest : IRequest<ListStudentByIdResponse>
{
    public Guid? Id { get; set; }
}

public class ListStudentByIdResponse
{
    public StudentApiResponse Student { get; set; } = new StudentApiResponse();

    public bool Success { get; set; }
}

public class ListStudentByIdRequestHandler(IStudentRepository studentRepository)
    : IRequestHandler<ListStudentByIdRequest, ListStudentByIdResponse>
{
    public async Task<ListStudentByIdResponse> Handle(ListStudentByIdRequest request, CancellationToken cancellationToken)
    {
        var response = new ListStudentByIdResponse { Success = false };
        var student = await studentRepository.FindById(request.Id!.Value, cancellationToken);
        if (student == null) return response;

        response.Success = true;
        response.Student = new
            StudentApiResponse
        {
            Id = student.Id,
            Firstname = student.Firstname,
            Lastname = student.Lastname,
            DateOfBirth = student.DateOfBirth.ToString()
        };
        return response;
    }
}

public class ListStudentByIdRequestValidator : AbstractValidator<ListStudentByIdRequest>
{
    public ListStudentByIdRequestValidator()
    {
        RuleFor(p => p.Id).Must(p => p != Guid.Empty)
            .WithMessage(ValidationMessages.InvalidId);
    }
}
