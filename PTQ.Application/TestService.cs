using PTQ.Models;
using PTQ.Repositories;
using PTQ.Repositories.dtos;

namespace PTQ.Application;

public class TestService
{
    public string _connectionString;
    public TestRepository _repository;

    public TestService(string connectionString)
    {
        _connectionString = connectionString;
        _repository = new TestRepository(connectionString);
    }

    public List<QuizDto> GetQuizzes()
    {
        var quizzes = _repository.GetQuizzes();

        return quizzes;
    }

    public QuizReturnDto GetQuiz(int id)
    {
        return _repository.GetQuiz(id);
    }

    public void AddQuiz(QuizCreateDto quiz)
    {
        _repository.AddQuiz(quiz);
    }
}