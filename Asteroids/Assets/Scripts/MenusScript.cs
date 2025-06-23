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
    private int scorePoints = 0;

    public static bool isGamePaused {get; private set;} = false;
    public bool isGameOver {get; private set;} = false;

    void Awake()
    {
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && MenusScript.isGamePaused == false){
            SetPause(true);
        }else if(Input.GetKeyDown(KeyCode.Escape) && MenusScript.isGamePaused){
            SetPause(false);
        }
    }

    public void SetPause(bool pause){
        isGamePaused = pause;
        pauseScreen.SetActive(pause);
        if(pause){
            Time.timeScale = 0f;
        }else{
            Time.timeScale = 1f;
        }
    }

    public int GetScorePoints(){
        return scorePoints;
    }
    public void SetGameOver(bool gameover){
        int currentScore = scorePoints;
        isGameOver = gameover;
        LeaderboardDataScript currentLeaderboard = LeaderboardManager.Instance.LoadLeaderboard();
        if(currentLeaderboard.highscores.Count < 10){
            scoreTextHighscoreEntry.text = currentScore.ToString();
            livesText.SetActive(false);
            pointsText.SetActive(false);
            highscoreEntry.SetActive(true);
            return;
        }else{
            foreach(var highscore in currentLeaderboard.highscores){
                if(currentScore > highscore.highscore){
                    scoreTextHighscoreEntry.text = currentScore.ToString();
                    livesText.SetActive(false);
                    pointsText.SetActive(false);
                    highscoreEntry.SetActive(true);
                    return;
                }
            }
        }
        scoreTextGameOver.text = currentScore.ToString();
        livesText.SetActive(false);
        pointsText.SetActive(false);
        gameOverScreen.SetActive(gameover);
    }
    public void UpdatePoints(int points){
        scorePoints += points;
        scoreText.text = scorePoints.ToString();
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
