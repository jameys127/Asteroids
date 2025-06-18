using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreTemplateScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI posText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI nameText;
    // Start is called before the first frame update

    public void SetTemplateText(int pos, int score, string name){
        posText.text = pos.ToString();
        scoreText.text = score.ToString();
        nameText.text = name;
    }
}
