using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace LearningDotNet.Application.Features.Students;

public class StudentEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup($"{ApiEndpoints.ApiPrefix}/{ApiEndpoints.ApiVersion}/{ApiEndpoints.StudentsApiName}");
        group.MapPost(ApiActions.Create,
                async (CreateStudentRequest request, IMediator mediator)
                    => await mediator.Send(request))
            .WithName("CreateStudent")
            .WithOpenApi();

        group.MapPut(ApiActions.Edit,
                async (EditStudentRequest request, IMediator mediator)
                    => await mediator.Send(request))
            .WithName("EditStudent")
            .WithOpenApi();

        group.MapDelete($"{ApiActions.Delete}/{{id}}",
                async (Guid id, IMediator mediator)
                    => await mediator.Send(new DeleteStudentRequest { Id = id }))
            .WithName("DeleteStudent")
            .WithOpenApi();

        group.MapGet(ApiActions.List,
                async (IMediator mediator)
                    => await mediator.Send(new ListStudentsRequest()))
            .WithName("ListStudents")
            .WithOpenApi();

        group.MapGet($"{ApiActions.List}/{{id}}",
                async (Guid id, IMediator mediator)
                    => await mediator.Send(new ListStudentByIdRequest { Id = id }))
            .WithName("ListStudentById")
            .WithOpenApi();
    }
}
