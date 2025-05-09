using PTQ.Application;
using PTQ.Models;
using PTQ.Repositories.dtos;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("PTQ_Database");

if (connectionString == null)
{
    Console.WriteLine("Connection string not defined");
    Environment.Exit(-1);
}

builder.Services.AddSingleton<TestService>(sp => new TestService(connectionString));
var app = builder.Build();

app.MapGet("/api/quizzes", (TestService service) =>
{
    try
    {
        return Results.Ok(service.GetQuizzes());
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.MapGet("/api/quizzes/{id}", (int id, TestService service) =>
{
    try
    {
        return Results.Ok(service.GetQuiz(id));
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.MapPost("api/quizzes", (TestService service, QuizCreateDto quiz) =>
{
    try
    {
        service.AddQuiz(quiz);
        return Results.Created();
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.UseHttpsRedirection();
app.Run();