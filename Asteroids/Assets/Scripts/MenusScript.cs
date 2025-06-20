using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenusScript : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject pauseScreen;
    public GameObject highscoreEntry;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreTextGameOver;
    public TextMeshProUGUI scoreTextHighscoreEntry;
    public GameObject livesText;
    public GameObject pointsText;
    public static MenusScript instance;
    private int scorePoints = 0;

    public static bool isGamePaused {get; private set;} = false;
    public static bool isGameOver {get; private set;} = false;

    void Awake()
    {
        instance = this;   
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && MenusScript.isGamePaused == false){
            SetPause(true);
        }else if(Input.GetKeyDown(KeyCode.Escape) && MenusScript.isGamePaused){
            SetPause(false);
        }
    }

    public static void SetPause(bool pause){
        isGamePaused = pause;
        instance.pauseScreen.SetActive(pause);
        if(pause){
            Time.timeScale = 0f;
        }else{
            Time.timeScale = 1f;
        }
    }

    public int GetScorePoints(){
        return instance.scorePoints;
    }
    public static void SetGameOver(bool gameover){
        int currentScore = instance.scorePoints;
        isGameOver = gameover;
        LeaderboardDataScript currentLeaderboard = LeaderboardManager.Instance.LoadLeaderboard();
        if(currentLeaderboard.highscores.Count < 10){
            instance.scoreTextHighscoreEntry.text = currentScore.ToString();
            instance.livesText.SetActive(false);
            instance.pointsText.SetActive(false);
            instance.highscoreEntry.SetActive(true);
            return;
        }else{
            foreach(var highscore in currentLeaderboard.highscores){
                if(currentScore > highscore.highscore){
                    instance.scoreTextHighscoreEntry.text = currentScore.ToString();
                    instance.livesText.SetActive(false);
                    instance.pointsText.SetActive(false);
                    instance.highscoreEntry.SetActive(true);
                    return;
                }
            }
        }
        instance.scoreTextGameOver.text = currentScore.ToString();
        instance.livesText.SetActive(false);
        instance.pointsText.SetActive(false);
        instance.gameOverScreen.SetActive(gameover);
    }
    public static void UpdatePoints(int points){
        instance.scorePoints += points;
        instance.scoreText.text = instance.scorePoints.ToString();
    }
    public void PlayAgain(){
        isGameOver = false;
        isGamePaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Quit(){
        isGameOver = false;
        isGamePaused = false;
        SceneManager.LoadScene("MainMenu");
    }
}
