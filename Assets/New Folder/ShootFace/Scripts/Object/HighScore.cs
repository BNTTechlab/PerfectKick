using System.Collections.Generic;

[System.Serializable]
public class Highscore
{
    public List<PlayerScore> scores;
}

[System.Serializable]
public class PlayerScore
{
    public string fileName;
    public int score;
    public PlayerScore(string fileName, int score)
    {
        this.fileName = fileName;
        this.score = score;
    }

}