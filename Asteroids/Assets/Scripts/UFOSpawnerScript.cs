using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOSpawnerScript : MonoBehaviour
{
    public GameObject gamelogicmanager;
    private GameLogicScript logic;
    public GameObject UFO;
    public bool isLeft;
    private float moveSpeed;
    private float shootSpeed;
    private float laserSpeed;

    private float cooldown;
    private bool startCooldown = false;

    // Start is called before the first frame update
    void Start()
    {
        logic = gamelogicmanager.GetComponent<GameLogicScript>();
        moveSpeed = 2f;
        shootSpeed = 2f;
        laserSpeed = 3f;
        cooldown = Random.Range(7, 45);
    }

    // Update is called once per frame
    void Update()
    {
        if(logic.GetWave() > 1 && !startCooldown){
            startCooldown = true;
        }
        if(startCooldown){
            cooldown -= Time.deltaTime;
        }
        if(cooldown <= 0){
            SpawnUFO();
            cooldown = Random.Range(15, 30);
            shootSpeed -= 0.1f;
            if(shootSpeed < 0.8f){
                shootSpeed = 0.8f;
            }
        }
    }

    void SpawnUFO(){
        if(isLeft){
            GameObject ufo = Instantiate(UFO, new Vector3(transform.position.x, Random.Range(-4, 4), 0), transform.rotation);
            ufo.GetComponent<UFOScript>().SetupDanger(Vector2.right, moveSpeed, shootSpeed, laserSpeed);
        }else{
            GameObject ufo = Instantiate(UFO, new Vector3(transform.position.x, Random.Range(-4, 4), 0), transform.rotation);
            ufo.GetComponent<UFOScript>().SetupDanger(Vector2.left, moveSpeed, shootSpeed, laserSpeed);
        }
    }
}
