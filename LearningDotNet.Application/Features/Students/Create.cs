﻿using FluentValidation;
using LearningDotNet.Domain.Entities;
using LearningDotNet.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Builder;

namespace LearningDotNet.Application.Features.Students;

public static class CreateStudentApiEndpoint
{
    public static void ConfigureStudentCreateApiEndpoint(this WebApplication webApplication)
    {
        webApplication.MapPost(
                $"{ApiEndpoints.ApiPrefix}/{ApiEndpoints.ApiVersion}/{ApiEndpoints.StudentsApiName}/{ApiActions.Create}",
                async (CreateStudentRequest request, IMediator mediator)
                => await mediator.Send(request))
            .WithName("CreateStudent")
          .WithOpenApi();
    }
}

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
        var student = new Student(request.Firstname, request.Lastname, DateOnly.Parse(request.DateOfBirth));
        studentRepository.Add(student);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return new CreateStudentResponse {Success = true};
    }
}

public class CreateStudentRequestValidator : AbstractValidator<CreateStudentRequest>
{
    public CreateStudentRequestValidator()
    {
        RuleFor(p => p.Firstname).NotEmpty().MaximumLength(50);
        RuleFor(p => p.Lastname).NotEmpty().MaximumLength(50);
        RuleFor(p => p.DateOfBirth).Must(BeAValidDate).WithMessage("Date of birth must be in the format dd/mm/yyyy.");
    }

    private bool BeAValidDate(string value)
    {
        DateOnly date;
        return DateOnly.TryParse(value, out date);
    }
}