namespace PTQ.Repositories.dtos;

public class QuizReturnDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string TeacherName { get; set; }
    public string Path { get; set; }

    public QuizReturnDto(int id, string name, string teacherName, string path)
    {
        Id = id;
        Name = name;
        TeacherName = teacherName;
        Path = path;
    }
}