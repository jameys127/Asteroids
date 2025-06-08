using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleScript : MonoBehaviour
{
    private float speed = 10f;
    private float timeToDespawn = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
        if(timeToDespawn <= 0){
            Destroy(this.gameObject);
        }else{
            timeToDespawn -= Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Asteroid")){
            Destroy(this.gameObject);
        }
    }
}
