using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    private float speed;
    private Vector2 direction;
    private float timeToDespawn = 3f;
    // Start is called before the first frame update
    void Start()
    {
        float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(rotation -90f, Vector3.forward);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
        if(timeToDespawn <=0){
            Destroy(gameObject);
        }else{
            timeToDespawn -= Time.deltaTime;
        }
    }

    public void SetSpeedAndDirection(float speed, Vector2 direction){
        this.speed = speed;
        this.direction = direction;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Asteroid") || collision.CompareTag("Spaceship") ||
           collision.CompareTag("ScreenWrapTopBottom") || collision.CompareTag("ScreenWrapLeftRight")){
            Destroy(gameObject);
        }
    }
}
