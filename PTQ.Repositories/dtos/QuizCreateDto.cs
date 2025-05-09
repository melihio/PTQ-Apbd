namespace PTQ.Repositories.dtos;

public class QuizCreateDto
{
    public string name { get; set; }
    public string teacherName { get; set; }
    public string path { get; set; }

    public QuizCreateDto(string name, string teacherName, string path)
    {
        this.name = name;
        this.teacherName = teacherName;
        this.path = path;
    }
}