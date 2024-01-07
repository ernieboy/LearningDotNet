using LearningDotNet.Domain.Interfaces;
using MediatR;

namespace LearningDotNet.Application.Features.Students;

public class ListStudentsRequest : IRequest<ListStudentsResponse>;

public class ListStudentsResponse
{
    public IEnumerable<StudentApiResponse> Students { get; set; } = new List<StudentApiResponse>();

    public bool Success { get; set; }
}

public class StudentApiResponse
{
    public Guid? Id { get; set; }

    public string Firstname { get; set; } = string.Empty;

    public string Lastname { get; set; } = string.Empty;

    public string DateOfBirth { get; set; } = string.Empty;
}

public class ListStudentsRequestHandler(
        IStudentRepository studentRepository)
    : IRequestHandler<ListStudentsRequest, ListStudentsResponse>
{
    public async Task<ListStudentsResponse> Handle(ListStudentsRequest request, CancellationToken cancellationToken)
    {
        var students = await studentRepository.FindAll(cancellationToken);
        var apiStudents = new List<StudentApiResponse>();

        apiStudents.AddRange(students.Select(s=> new
            StudentApiResponse
        {
            Id = s!.Id,
            Firstname = s.Firstname,
            Lastname = s.Lastname,
            DateOfBirth = s.DateOfBirth.ToString(),
        }));
        return new ListStudentsResponse { Students = apiStudents, Success = true };
    }
}