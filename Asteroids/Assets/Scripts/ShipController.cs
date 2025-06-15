using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipController : MonoBehaviour
{
    //Keyboard Input
    float xInput;
    float yInput;
    private float maxSpeed = 12f;
    private float acceleration = 7f;
    private float drag = 1f;
    private Vector2 velocity = Vector2.zero;
    private float rotationspeed = 250f;

    //Missles
    public GameObject missle;
    private float missleCooldown;
    private float missleOffsetY = 0.4f;
    private float missleOffsetX = 0.06f;

    //Booleans
    private bool isAlive = true;
    private bool isInvulnerable = false;

    //GameObject references
    private Animator animator;
    private ParticleSystem deathParticles;
    private GameLogicScript logic;
    private SpriteRenderer spriteRenderer;


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
        if(!isAlive) return;

        if(yInput > 0){
            Vector2 thrustDirection = transform.up;
            velocity += thrustDirection * acceleration * Time.deltaTime;
        }

        velocity = velocity *(1f - drag * Time.deltaTime);

        if(velocity.magnitude > maxSpeed){
            velocity = velocity.normalized * maxSpeed;
        }

        transform.Translate(velocity * Time.deltaTime, Space.World);
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
        velocity = Vector2.zero;
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
