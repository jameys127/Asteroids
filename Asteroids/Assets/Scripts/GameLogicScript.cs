using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameLogicScript : MonoBehaviour
{

    public GameObject[] spawners;
    private int difficulty = 4;
    private List<GameObject> asteroidsInPlay = new List<GameObject>();
    private int wave = 1;
    private float asteroidSpeed = 1f;
    public TextMeshProUGUI score;
    public List<Image> lifeRenderers;
    private int lives;


    // Start is called before the first frame update
    void Start()
    {
        lives = 3;
        StartCoroutine(StartGame());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartGame(){
        while(true){
            yield return new WaitForSeconds(1f);
            RandomlySpawn();
            yield return new WaitUntil(CheckGameState);
            yield return new WaitForSeconds(0.1f);
            if(MenusScript.isGameOver){
                yield break;
            }else{
                wave++;
                if(wave > 12){
                    continue;
                }else{
                    asteroidSpeed += 0.2f;
                }
                if(wave == 3 || wave == 6 || wave == 9 || wave == 12){
                    difficulty++;
                }
            }
        }
    }

    public void AddAsteroidsInPlay(GameObject asteroid){
        asteroidsInPlay.Add(asteroid);
    }
    public void RemoveAsteroidInPlay(GameObject asteroid){
        asteroidsInPlay.Remove(asteroid);
    }

    public int GetWave(){
        return wave;
    }

    public void RemoveLifeTotal(){
        if(lives > 1){
            lifeRenderers[lives - 1].enabled = false;
            lives--;
        }else{
            lifeRenderers[0].enabled = false;
            MenusScript.SetGameOver(true);
        }
    }

    bool CheckGameState(){
        if(asteroidsInPlay.Count == 0){
            return true;
        }else if(MenusScript.isGameOver){
            return true;
        }else{
            return false;
        }
    }

    void RandomlySpawn(){
        List<int> index = new List<int>();
        int counter = 0;

        while(counter < difficulty){
            int randomIndex = Random.Range(0, 13);
            if((index.Count == 0 || index.Count > 0) && !index.Contains(randomIndex)){
                index.Add(randomIndex);
                GameObject asteroid = spawners[randomIndex].GetComponent<AsteroidSpawnerScript>().SpawnAsteroid();
                asteroid.GetComponent<AsteroidScript>().IncreaseSpeed(asteroidSpeed);
                asteroidsInPlay.Add(asteroid);
                counter++;
            }
        }
    }
}
