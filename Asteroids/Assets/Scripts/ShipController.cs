using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    float xInput;
    float yInput;
    public float rotationspeed;
    public GameObject missle;
    public float missleCooldown;
    private float maxSpeed = 5f;
    private float accelerationTime = 3f;
    private float deccelerationTime = 7f;
    private float brakeDeccel = 1f;
    private float acceleration;
    private float velocity;


    private float missleOffsetY = 0.4f;
    private float missleOffsetX = 0.06f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        UpdateMovement();
        UpdateRotation();
        ShootMissle();
    }

    void GetInput(){
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxis("Vertical");
    }

    void UpdateMovement(){
        if(yInput > 0){
            acceleration = maxSpeed / accelerationTime;
            velocity += acceleration * Time.deltaTime;
            if(velocity > maxSpeed){
                velocity = maxSpeed;
            }
            transform.Translate(Vector2.up * velocity * Time.deltaTime);
        }else if(yInput < 0){
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
    }

    void UpdateRotation(){
        if(Mathf.Abs(xInput) > 0){
            transform.Rotate(0, 0, rotationspeed * -xInput * Time.deltaTime);
        }
    }

    void ShootMissle(){
        if(Input.GetKey(KeyCode.Space) && missleCooldown <= 0){
            Vector3 spawnPos = transform.position + transform.up * missleOffsetY + transform.right * missleOffsetX;
            Instantiate(missle, spawnPos, transform.rotation);
            missleCooldown = 0.2f;
        }
        if(missleCooldown > 0){
            missleCooldown -= Time.deltaTime;
        }
    }
}
