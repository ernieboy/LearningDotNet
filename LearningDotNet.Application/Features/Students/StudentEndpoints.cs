﻿using Carter;
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
                async (EditStudentRequest request, Guid id, IMediator mediator)
                    =>
                {
                    request.Id = id;
                    return await mediator.Send(request);
                })
            .WithName("EditStudent")
            .WithOpenApi();
    }
}