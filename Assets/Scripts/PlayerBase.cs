public class PlayerBase
{
    public string Id;
    public string name;
    public int BestScore;
    public int Coin;
    public byte Level;
    public string SpriteHouse;
    public string SpriteBullet;
    public string SpritePlayer;
    public string SpriteGround;
    public string spriteBGround;


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