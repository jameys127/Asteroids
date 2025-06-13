using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameLogicScript : MonoBehaviour
{

    public GameObject[] spawners;
    private int difficulty = 5;
    private List<GameObject> asteroidsInPlay = new List<GameObject>();
    public TextMeshProUGUI score;
    public List<Image> lifeRenderers;
    private int lives = 3;
    private int scorePoints = 0;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartGame());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartGame(){
        while(true){
            yield return new WaitForSeconds(1f);
            Debug.Log("starting game");
            RandomlySpawn();
            yield return new WaitUntil(() => asteroidsInPlay.Count == 0);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void AddAsteroidsInPlay(GameObject asteroid){
        asteroidsInPlay.Add(asteroid);
    }
    public void RemoveAsteroidInPlay(GameObject asteroid){
        asteroidsInPlay.Remove(asteroid);
    }

    public void RemoveLifeTotal(){
        if(lives > 0){
            lifeRenderers[lives - 1].enabled = false;
            lives--;
        }else{
            //game over
        }
    }

    void RandomlySpawn(){
        List<int> index = new List<int>();
        int counter = 0;

        while(counter < difficulty){
            int randomIndex = Random.Range(0, 13);
            if((index.Count == 0 || index.Count > 0) && !index.Contains(randomIndex)){
                index.Add(randomIndex);
                asteroidsInPlay.Add(spawners[randomIndex].GetComponent<AsteroidSpawnerScript>().SpawnAsteroid());
                counter++;
            }
        }
    }

    public void UpdatePoints(int points){
        scorePoints += points;
        score.text = scorePoints.ToString();
    }
}
