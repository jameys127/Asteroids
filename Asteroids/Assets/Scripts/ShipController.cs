using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipController : MonoBehaviour
{
    float xInput;
    float yInput;
    public float rotationspeed;
    public GameObject missle;
    public float missleCooldown;
    private float maxSpeed = 5f;
    private float accelerationTime = 2f;
    private float deccelerationTime = 2.5f;
    private float brakeDeccel = 1f;
    private float acceleration;
    private float velocity;
    private bool isAlive = true;
    private bool isInvulnerable = false;
    private Animator animator;
    private ParticleSystem deathParticles;
    public GameLogicScript logic;
    private SpriteRenderer spriteRenderer;


    private float missleOffsetY = 0.4f;
    private float missleOffsetX = 0.06f;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        logic = GameObject.FindGameObjectWithTag("GameLogicManager").GetComponent<GameLogicScript>();
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

    IEnumerator Respawn(){
        yield return new WaitForSeconds(2f);
        logic.RemoveLifeTotal();
        if(MenusScript.isGameOver){
            yield break;
        }
        isAlive = true;
        transform.position = new Vector3(0, 0, transform.position.z);
        transform.rotation = Quaternion.identity;
        spriteRenderer.enabled = true;
        yield return new WaitForSeconds(0.4f);
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(0.4f);
        spriteRenderer.enabled = true;
        yield return new WaitForSeconds(0.4f);
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(0.4f);
        spriteRenderer.enabled = true;
        yield return new WaitForSeconds(0.4f);
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(0.4f);
        spriteRenderer.enabled = true;
        yield return new WaitForSeconds(0.4f);
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(0.4f);
        spriteRenderer.enabled = true;
        isInvulnerable = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Asteroid") && !isInvulnerable){
            spriteRenderer.enabled = false;
            PlayDeathParticles();
            isAlive = false;
            isInvulnerable = true;
            StartCoroutine(Respawn());
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
