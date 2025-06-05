using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public float rotationspeed;
    private float maxSpeed = 5f;
    private float accelerationTime = 3f;
    private float deccelerationTime = 7f;
    private float brakeDeccel = 1f;
    private float acceleration;
    private float velocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.UpArrow)){
            acceleration = maxSpeed / accelerationTime;
            velocity += acceleration * Time.deltaTime;
            if(velocity > maxSpeed){
                velocity = maxSpeed;
            }
            transform.Translate(Vector2.up * velocity * Time.deltaTime);
        }else if(Input.GetKey(KeyCode.DownArrow)){
            acceleration = -maxSpeed / brakeDeccel;
            velocity += acceleration * Time.deltaTime;
            if(velocity < 0){
                velocity = 0;
            }
            transform.Translate(Vector2.up * velocity * Time.deltaTime);
        }else{
            acceleration = -maxSpeed / deccelerationTime;
            velocity += acceleration * Time.deltaTime;
            if(velocity < 0){
                velocity = 0;
            }
            transform.Translate(Vector2.up * velocity * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.LeftArrow)){
            transform.Rotate(0, 0, rotationspeed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.RightArrow)){
            transform.Rotate(0, 0, -rotationspeed * Time.deltaTime);
        }
    }

    private void shootMissle(){
        
    }
}
