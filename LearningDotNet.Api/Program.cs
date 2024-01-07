using Carter;
using FluentValidation;
using LearningDotNet.Api.Behaviours;
using LearningDotNet.Application.Features.Students;
using LearningDotNet.Common.Middleware;
using LearningDotNet.Infrastructure.DependencyInjection;
using MediatR;

namespace LearningDotNet.Api;
public class Program
{
    protected Program()
    {
    }
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddCarter();

        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateStudentRequestHandler>());
        builder.Services.AddValidatorsFromAssembly(typeof(CreateStudentRequestValidator).Assembly);
        builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        builder.Services.RegisterServices(builder);
        var app = builder.Build();

        app.UseMiddleware<ValidationExceptionHandlingMiddleware>();
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.MapCarter();

        app.Run();
    }
}