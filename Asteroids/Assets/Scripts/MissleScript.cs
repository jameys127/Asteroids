using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleScript : MonoBehaviour
{
    private float speed = 10f;
    private float timeToDespawn = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
        if(timeToDespawn <= 0){
            Destroy(gameObject);
        }else{
            timeToDespawn -= Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Asteroid")){
            Destroy(gameObject);
        }
        if(collision.CompareTag("ScreenWrapTopBottom")){
            float offset = collision.transform.position.y > 0 ? -1f : 1f;
            transform.position = new Vector3(transform.position.x, -transform.position.y - offset, transform.position.z);
        }else if(collision.CompareTag("ScreenWrapLeftRight")){
            float offset = collision.transform.position.x > 0 ? -1f : 1f;
            transform.position = new Vector3(-transform.position.x - offset, transform.position.y, transform.position.z);
        }
    }
}
