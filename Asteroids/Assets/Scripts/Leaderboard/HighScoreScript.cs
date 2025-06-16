[System.Serializable]
public class HighScoreScript
{
    public int highscore;
    public string name;
    public HighScoreScript() {}
    public HighScoreScript(int highscore, string name){
        this.highscore = highscore;
        this.name = name;
    }
}
