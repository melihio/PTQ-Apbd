namespace PTQ.Models;

public class Quiz
{
    public int Id;
    public string Name;
    public int PotatoTeacherId;
    public string PathFile;

    public Quiz(int id, string name, int potatoTeacherId, string pathFile)
    {
        Id = id;
        Name = name;
        PotatoTeacherId = potatoTeacherId;
        PathFile = pathFile;
    }
}