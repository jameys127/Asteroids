using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawnerScript : MonoBehaviour
{
    public GameObject[] asteroids;

    void SpawnAsteroid(){
        Instantiate(asteroids[Random.Range(0,4)], transform.position, transform.rotation);
    }
}
