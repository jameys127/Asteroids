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
    private float deccelerationTime = 2.5f;
    private float brakeDeccel = 1f;
    private float acceleration;
    private float velocity;
    private bool isAlive = true;
    private int lives = 3;
    private Animator animator;
    private ParticleSystem deathParticles;


    private float missleOffsetY = 0.4f;
    private float missleOffsetX = 0.06f;

    // Start is called before the first frame update
    void Start()
    {
        deathParticles = GetComponent<ParticleSystem>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        UpdateMovement();
        UpdateAnimation();
        UpdateRotation();
        ShootMissle();
    }

    void GetInput(){
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxis("Vertical");
    }

    void UpdateAnimation(){
        bool isMoving = yInput > 0 && isAlive;
        animator.SetBool("IsMoving", isMoving);
    }

    void UpdateMovement(){
        if(yInput > 0 && isAlive){
            acceleration = maxSpeed / accelerationTime;
            velocity += acceleration * Time.deltaTime;
            if(velocity > maxSpeed){
                velocity = maxSpeed;
            }
            transform.Translate(Vector2.up * velocity * Time.deltaTime);
        }else if(yInput < 0 && isAlive){
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
        if(Mathf.Abs(xInput) > 0 && isAlive){
            transform.Rotate(0, 0, rotationspeed * -xInput * Time.deltaTime);
        }
    }

    void ShootMissle(){
        if(Input.GetKey(KeyCode.Space) && missleCooldown <= 0 && isAlive){
            Vector3 spawnPos = transform.position + transform.up * missleOffsetY + transform.right * missleOffsetX;
            Instantiate(missle, spawnPos, transform.rotation);
            missleCooldown = 0.2f;
        }
        if(missleCooldown > 0){
            missleCooldown -= Time.deltaTime;
        }
    }

    void PlayDeathParticles(){
        velocity = 0;
        if(isAlive){
            deathParticles.Play();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Asteroid")){
            GetComponent<SpriteRenderer>().enabled = false;
            PlayDeathParticles();
            isAlive = false;
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
