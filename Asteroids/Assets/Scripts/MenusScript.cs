using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenusScript : MonoBehaviour
{
    public GameObject gameOverScreen;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreTextGameOver;
    public static MenusScript instance;
    private int scorePoints = 0;

    public static bool isGamePaused {get; private set;} = false;
    public static bool isGameOver {get; private set;} = false;

    void Awake()
    {
        instance = this;   
    }

    public static void SetPause(bool pause){
        isGamePaused = pause;
    }
    public static void SetGameOver(bool gameover){
        isGameOver = gameover;
        instance.scoreTextGameOver.text = instance.scorePoints.ToString();
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
