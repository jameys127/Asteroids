using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    private bool isLittle = false;
    private float asteroidSpeed = 2f;
    private float littleAsteroidSpeed = 4.5f;
    public GameObject[] littleAsteroids;
    Vector3 spawnOffset1;
    Vector3 spawnOffset2;
    Vector2 moveSpeed;
    int rotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float randomSpeed = Random.Range(0.5f, 1.5f);
        moveSpeed = randomDirection * randomSpeed * asteroidSpeed;
        rotateSpeed = Random.Range(30, 100);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveSpeed * Time.deltaTime, Space.World);
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }

    public void SetIsLittleToTrue(){
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float randomSpeed = Random.Range(0.8f, 2f);
        moveSpeed = randomDirection * randomSpeed * littleAsteroidSpeed;
        rotateSpeed = Random.Range(30, 100);
        isLittle = true;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Missile") && isLittle == false){
            spawnOffset1 = transform.position + transform.up * 0.2f + transform.right * 0.1f;
            spawnOffset2 = transform.position + transform.up * -0.2f + transform.right * -0.1f;
            GameObject asteroid1 = Instantiate(littleAsteroids[Random.Range(0,3)], spawnOffset1, transform.rotation);
            asteroid1.GetComponent<AsteroidScript>().SetIsLittleToTrue();
            GameObject asteroid2 = Instantiate(littleAsteroids[Random.Range(0,3)], spawnOffset2, transform.rotation);
            asteroid2.GetComponent<AsteroidScript>().SetIsLittleToTrue();
            Destroy(gameObject);
        }else if(collision.CompareTag("Missile") && isLittle){
            Destroy(gameObject);
        }
        if(collision.CompareTag("ScreenWrapTopBottom")){
            float offset = collision.transform.position.y > 0 ? -0.3f : 0.3f;
            transform.position = new Vector3(transform.position.x, -transform.position.y - offset, transform.position.z);
        }else if(collision.CompareTag("ScreenWrapLeftRight")){
            float offset = collision.transform.position.x > 0 ? -0.3f : 0.3f;
            transform.position = new Vector3(-transform.position.x - offset, transform.position.y, transform.position.z);
        }
    }
}
