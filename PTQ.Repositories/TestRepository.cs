using Microsoft.Data.SqlClient;
using PTQ.Models;
using PTQ.Repositories.dtos;

namespace PTQ.Repositories;

public class TestRepository
{
    private string _connectionString;

    public TestRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<QuizDto> GetQuizzes()
    {
        var quizzes = new List<QuizDto>();

        var conn = new SqlConnection(_connectionString);
        const string sql = "SELECT Id,Name FROM Quiz";
        var cmd = new SqlCommand(sql, conn);
        
        conn.Open();
        var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            var quiz = new QuizDto(reader.GetInt32(0), reader.GetString(1));
            quizzes.Add(quiz);
        }

        return quizzes;
    }

    public QuizReturnDto GetQuiz(int id)
    {
        var conn = new SqlConnection(_connectionString);
        const string sql = "SELECT * FROM Quiz WHERE Id = @Id";
        var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", id);
        
        conn.Open();
        var reader = cmd.ExecuteReader();
        

        if (reader.Read())
        {
            var teacherName = GetTeacherById(reader.GetInt32(2));
            var quiz = new QuizReturnDto(reader.GetInt32(0), reader.GetString(1),teacherName,reader.GetString(3));
            return quiz;
        }

        throw new KeyNotFoundException("Quiz not found");
    }

    public string GetTeacherById(int id)
    {
        var conn = new SqlConnection(_connectionString);
        const string sql = "SELECT Name FROM PotatoTeacher WHERE Id = @Id";
        var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", id);
        conn.Open();
        var reader = cmd.ExecuteReader();
        
        if (!reader.Read()) throw new KeyNotFoundException("Teacher not found");
        
        Console.WriteLine(reader.GetString(0));
        return reader.GetString(0);
    }
    
    public int GetTeacherIdByName(string name)
    {
        var conn = new SqlConnection(_connectionString);
        const string sql = "SELECT Id FROM PotatoTeacher WHERE Name = @Name";
        var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Name", name);
        conn.Open();
        var reader = cmd.ExecuteReader();
        
        if (!reader.Read()) throw new KeyNotFoundException("Teacher not found");
        
        return reader.GetInt32(0);
    }

    public void AddTeacher(string name)
    {
        var conn = new SqlConnection(_connectionString);
        
        const string sql = "INSERT INTO PotatoTeacher(Name)"+ 
                           "VALUES(@Name)";
        var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Name", name);
        
        conn.Open();
        cmd.ExecuteNonQuery();
    }

    public void AddQuiz(QuizCreateDto quiz)
    {
        var conn = new SqlConnection(_connectionString);
        
        var id = GetQuizzes().Count + 1;
        
        const string sql = "INSERT INTO Quiz(Name,PotatoTeacherId,PathFile)"+ 
                           "VALUES(@Name,@PotatoTeacherId,@PathFile)";
        var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Name", quiz.name);
        try
        {
            cmd.Parameters.AddWithValue("@PotatoTeacherId", GetTeacherIdByName(quiz.teacherName));
        }
        catch (KeyNotFoundException)
        {
            AddTeacher(quiz.teacherName);
            cmd.Parameters.AddWithValue("@PotatoTeacherId", GetTeacherIdByName(quiz.teacherName));
        }
        cmd.Parameters.AddWithValue("@PathFile", quiz.path);
        
        conn.Open();
        cmd.ExecuteNonQuery();
    }
}