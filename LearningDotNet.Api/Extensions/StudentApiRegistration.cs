using LearningDotNet.Application.Features.Students;
using MediatR;

namespace LearningDotNet.Api.Extensions;

public static class StudentApiRegistration
{
    public static void RegisterStudentApi(this WebApplication webApplication)
    {
        webApplication.MapPost("/api/v1/students/create", async (CreateStudentRequest request, IMediator mediator) =>
            {
               var response =  await mediator.Send(request);
            })
            .WithName("CreateStudent")
            .WithOpenApi();
    }
}