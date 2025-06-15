using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOScript : MonoBehaviour
{
    public GameObject laser;
    private Vector2 direction;
    private float shootSpeed;
    private float laserSpeed;
    private float moveSpeed;
    private float initialShootSpeed;
    private GameObject spaceship;

    // Start is called before the first frame update
    void Start()
    {
        spaceship = GameObject.FindGameObjectWithTag("Spaceship");
    }

    // Update is called once per frame
    void Update()
    {
        shootSpeed -= Time.deltaTime;
        transform.Translate(direction * moveSpeed * Time.deltaTime);
        if(shootSpeed <= 0){
            ShootMissile();
            shootSpeed = initialShootSpeed;
        }
    }

    public void SetupDanger(Vector2 direction, float moveSpeed, float shootSpeed, float laserSpeed){
        this.direction = direction;
        this.moveSpeed = moveSpeed;
        this.shootSpeed = shootSpeed;
        this.laserSpeed = laserSpeed;
        initialShootSpeed = shootSpeed;
    }

    void ShootMissile(){
        Vector2 laserDirection = spaceship.transform.position - gameObject.transform.position;
        GameObject shotLaser = Instantiate(laser, transform.position, Quaternion.identity);
        shotLaser.GetComponent<LaserScript>().SetSpeedAndDirection(laserSpeed, laserDirection.normalized);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("ScreenWrapLeftRight")||
           collision.CompareTag("Spaceship")){
            Debug.Log("i was hit by spaceship");
            Destroy(gameObject);
        }
        if(collision.CompareTag("Asteroid")){
            Debug.Log("i was hit");
        }
        if(collision.CompareTag("Missile")){
            MenusScript.UpdatePoints(200);
            Destroy(gameObject);
        }
    }

}
