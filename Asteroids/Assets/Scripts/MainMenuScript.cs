using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void Play(){
        SceneManager.LoadScene("GameplayScene");
    }

    public void Leaderboard(){
        SceneManager.LoadScene("LeaderboardScene");
    }
    public void Back(){
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit(){
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
