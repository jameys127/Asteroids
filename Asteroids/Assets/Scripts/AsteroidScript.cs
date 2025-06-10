using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    private bool isLittle = false;
    private float asteroidSpeed = 1f;
    public GameObject[] littleAsteroids;
    Vector3 spawnOffset1;
    Vector3 spawnOffset2;
    Vector2 moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = new Vector2(0, asteroidSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveSpeed * Time.deltaTime, Space.World);
        transform.Rotate(0, 0, 100 * Time.deltaTime);
    }

    public void SetIsLittleToTrue(){
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
