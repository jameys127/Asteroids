using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class LeaderboardManager
{
    private static LeaderboardManager _instance;
    public static LeaderboardManager Instance{
        get{
            if(_instance == null){
                _instance = new LeaderboardManager();
            }
            return _instance;
        }
    }
    public LeaderboardManager() {}

    public LeaderboardDataScript LoadLeaderboard(){
        //the file path "persistent path"
        string filePath = Application.persistentDataPath + "/leaderboard.json";

        if(File.Exists(filePath)){
            try{
                //read the file
                string jsonString = File.ReadAllText(filePath);
                //deserialize the json to the object
                LeaderboardDataScript data = JsonUtility.FromJson<LeaderboardDataScript>(jsonString);
                //return the object
                return data;
            }catch(System.Exception e){
                Debug.LogError("Error loading leaderboard: " + e);
                return new LeaderboardDataScript();
            }
        }else{
            //first time no leaderboard exists yet
            return new LeaderboardDataScript();
        }
    }
    public void SaveLeaderboard(LeaderboardDataScript data){
        string filePath = Application.persistentDataPath + "/leaderboard.json";
        try{
            //converts data to json. the boolean "true" is ta parameter for "pretty print"
            //makes the json more readable
            string jsonString = JsonUtility.ToJson(data, true);
            File.WriteAllText(filePath, jsonString);
            Debug.Log("Leaderboard Saved Successfully!");
        }catch(System.Exception e){
            Debug.LogError("Error saving to leaderboard" + e);
        }
    }

    public void AddNewScore(int score, string name){
        LeaderboardDataScript data = LoadLeaderboard();
        HighScoreScript newScore = new HighScoreScript(score, name);
        data.highscores.Add(newScore);
        data.highscores = data.highscores.OrderByDescending(x => x.highscore).ToList();
        if(data.highscores.Count > 10){
            data.highscores = data.highscores.GetRange(0, 10);
        }
        SaveLeaderboard(data);
        Debug.Log("Leaderboard path: " + Application.persistentDataPath + "/leaderboard.json");
    }
}
