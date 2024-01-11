public class PlayerBase
{
    public string Id;
    public string name;
    public int BestScore;

    public PlayerBase(string id, string name)
    {
        Id = id;
        this.name = name;
    }

    public PlayerBase(string id, string name, int bestScore)
    {
        Id = id;
        this.name = name;
        BestScore = bestScore;
    }
}