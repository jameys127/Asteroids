using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardMenu : MonoBehaviour
{
    public GameObject scoreTemplateContainer;
    public GameObject scoreTemplate;
    // Start is called before the first frame update
    void Awake()
    {
        LeaderboardDataScript data = LeaderboardManager.Instance.LoadLeaderboard();
        scoreTemplate.SetActive(false);
        float templateHeight = 60f;
        for(int i = 0; i < 10; i++){
            if(data.highscores.Count - 1 >= i){
                HighScoreScript varHighScore = data.highscores[i];
                GameObject varScoreWithScore = Instantiate(scoreTemplate, scoreTemplateContainer.transform);
                varScoreWithScore.GetComponent<ScoreTemplateScript>().SetTemplateText(i + 1, varHighScore.highscore, varHighScore.name);
                RectTransform varScoreWithScoreTransfrom = varScoreWithScore.GetComponent<RectTransform>();
                varScoreWithScoreTransfrom.anchoredPosition = new Vector2(0, -templateHeight * i);
                varScoreWithScore.SetActive(true);
            }else{
                GameObject varScore = Instantiate(scoreTemplate, scoreTemplateContainer.transform);
                RectTransform varScoreTransform = varScore.GetComponent<RectTransform>();
                varScoreTransform.anchoredPosition = new Vector2(0, -templateHeight * i);
                varScore.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
