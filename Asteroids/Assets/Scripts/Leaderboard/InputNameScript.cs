using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputNameScript : MonoBehaviour
{
    [Header("The value from the input field")]
    [SerializeField] private TMP_InputField inputText;
    [SerializeField] private GameObject gameLogicManager;
    private MenusScript menuscript;
    private string input;

    public void GrabFromInputField(){
        input = inputText.text;
        Debug.Log("inputText set to: '" + inputText.text + "'");
    }

    public void SubmitHighscore(){
        menuscript = gameLogicManager.GetComponent<MenusScript>();
        GrabFromInputField();
        if(string.IsNullOrEmpty(input)){
            input = "AAA";
            Debug.Log("Set to AAA because null/empty");
        }
        LeaderboardManager.Instance.AddNewScore(menuscript.GetScorePoints(), input);
        SceneManager.LoadScene("MainMenu");
    }

}
