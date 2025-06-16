using System.Collections.Generic;
[System.Serializable]
public class LeaderboardDataScript
{
    public List<HighScoreScript> highscores;

    public LeaderboardDataScript(){
        highscores = new List<HighScoreScript>();
    }
}
