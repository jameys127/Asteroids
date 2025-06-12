using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawnerScript : MonoBehaviour
{
    public GameObject[] asteroids;

    public GameObject SpawnAsteroid(){
        GameObject asteroid = Instantiate(asteroids[Random.Range(0,4)], transform.position, transform.rotation);
        return asteroid;
    }
}
