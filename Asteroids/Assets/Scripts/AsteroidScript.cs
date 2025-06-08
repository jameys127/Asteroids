using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    private bool isLittle = false;
    public GameObject[] littleAsteroids;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetIsLittleToTrue(){
        isLittle = true;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Missile")){
            Destroy(gameObject);
        }
    }
}
