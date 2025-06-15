using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    private bool isLittle = false;
    private float asteroidSpeed;
    public GameObject[] littleAsteroids;
    public GameObject deathParticles;
    public GameLogicScript logic;
    Vector3 spawnOffset1;
    Vector3 spawnOffset2;
    Vector2 moveSpeed;
    int rotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("GameLogicManager").GetComponent<GameLogicScript>();
        if(!isLittle){
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            float randomSpeed = Random.Range(0.5f, 1.5f);
            moveSpeed = randomDirection * randomSpeed * asteroidSpeed;
        }
        rotateSpeed = Random.Range(30, 100);
    }

    // Update is called once per frame
    void Update()
    {
        CheckOutOfBounds();
        transform.Translate(moveSpeed * Time.deltaTime, Space.World);
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }

    public void SetIsLittleToTrue(float passingSpeed){
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float randomSpeed = Random.Range(0.8f, 2f);
        moveSpeed = randomDirection * randomSpeed * passingSpeed;
        rotateSpeed = Random.Range(30, 100);
        isLittle = true;
    }
    void CheckOutOfBounds(){
        if(transform.position.x > 11.5 || transform.position.x < -11.5){
            transform.position = new Vector3(0, -5.5f, transform.position.z);
        }else if(transform.position.y > 7.5 || transform.position.y < -7.5){
            transform.position = new Vector3(0, -5.5f, transform.position.z);
        }
    }

    public void IncreaseSpeed(float asteroidIncrease){
        asteroidSpeed = asteroidIncrease;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Missile") && isLittle == false){
            spawnOffset1 = transform.position + transform.up * 0.2f + transform.right * 0.1f;
            spawnOffset2 = transform.position + transform.up * -0.2f + transform.right * -0.1f;

            GameObject asteroid1 = Instantiate(littleAsteroids[Random.Range(0,3)], spawnOffset1, transform.rotation);
            logic.AddAsteroidsInPlay(asteroid1);
            asteroid1.GetComponent<AsteroidScript>().SetIsLittleToTrue(asteroidSpeed);

            GameObject asteroid2 = Instantiate(littleAsteroids[Random.Range(0,3)], spawnOffset2, transform.rotation);
            logic.AddAsteroidsInPlay(asteroid2);
            asteroid2.GetComponent<AsteroidScript>().SetIsLittleToTrue(asteroidSpeed);

            MenusScript.UpdatePoints(50);

            logic.RemoveAsteroidInPlay(gameObject);
            Instantiate(deathParticles, transform.position, transform.rotation);
            Destroy(gameObject);
        }else if(collision.CompareTag("Missile") && isLittle){
            MenusScript.UpdatePoints(100);
            logic.RemoveAsteroidInPlay(gameObject);
            Instantiate(deathParticles, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        if(collision.CompareTag("ScreenWrapTopBottomLittle") && isLittle){
            float offset = collision.transform.position.y > 0 ? -0.4f : 0.2f;
            transform.position = new Vector3(transform.position.x, -transform.position.y - offset, transform.position.z);
        }else if(collision.CompareTag("ScreenWrapLeftRightLittle") && isLittle){
            float offset = collision.transform.position.x > 0 ? -0.2f : 0.2f;
            transform.position = new Vector3(-transform.position.x - offset, transform.position.y, transform.position.z);
        }

        
        if(collision.CompareTag("ScreenWrapTopBottom") && isLittle == false){
            float offset = collision.transform.position.y > 0 ? -0.3f : 0.3f;
            transform.position = new Vector3(transform.position.x, -transform.position.y - offset, transform.position.z);
        }else if(collision.CompareTag("ScreenWrapLeftRight") && isLittle == false){
            float offset = collision.transform.position.x > 0 ? -0.3f : 0.3f;
            transform.position = new Vector3(-transform.position.x - offset, transform.position.y, transform.position.z);
        }
    }
}
