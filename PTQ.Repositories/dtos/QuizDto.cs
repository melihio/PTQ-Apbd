namespace PTQ.Repositories.dtos;

public class QuizDto
{
    public int Id { get; set; }
    public string Name { get; set; }

    public QuizDto(int id, string name)
    {
        this.Id = id;
        this.Name = name;
    }
}