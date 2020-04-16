using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ClaraBehavior : MonoBehaviour
{
    [Header("Walking")]
    [SerializeField] private float horizontalAcceleration;
    [SerializeField] private float maximumHorizontalSpeed;
    [SerializeField] private float horizontalFriction;
    [SerializeField] private float drag;
    
    [Header("Jump")]
    [SerializeField] private float minInputForJump;
    [SerializeField] private float minJumpHeight;
    [SerializeField] private float maxInputForJump;
    [SerializeField] private float maxJumpHeight;
    private float minJumpSqrt;
    private float maxJumpSqrt;
    private float gravityCoEf;
    private bool onGround;
    private Rigidbody2D rb;
    [SerializeField] private float currentVelocity;
    [SerializeField] private float deathSpeed;
    private bool dead;
    [Header("Super secret programming magic")]
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private bool GodMode;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //TODO
        //rb.sharedMaterial.friction = horizontalFriction;
        //rb.drag = drag;
        minJumpSqrt = Mathf.Sqrt(minJumpHeight);
        maxJumpSqrt = Mathf.Sqrt(maxJumpHeight);
        gravityCoEf = Mathf.Sqrt(Physics2D.gravity.magnitude) * Mathf.Sqrt(2f);
    }
    void Update()
    {
        if (dead) return;
        if (inputHandler.leftStick.x_axis != 0f)
        {
            rb.velocity += new Vector2(inputHandler.leftStick.x_axis * horizontalAcceleration * Time.deltaTime, 0f);
        }
        if (rb.velocity.x > maximumHorizontalSpeed) rb.velocity = new Vector2(maximumHorizontalSpeed, rb.velocity.y);
        if (rb.velocity.x < -maximumHorizontalSpeed) rb.velocity = new Vector2(-maximumHorizontalSpeed, rb.velocity.y);


        if (inputHandler.leftTriggerAnalog.axis >= maxInputForJump && onGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, gravityCoEf * maxJumpSqrt);
            onGround = false;
        }
        else if (inputHandler.leftTriggerAnalog.axis >= minInputForJump && onGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, gravityCoEf * minJumpSqrt + gravityCoEf * ((maxJumpSqrt - minJumpSqrt) / maxJumpSqrt) * ((inputHandler.leftTriggerAnalog.axis - minInputForJump) / (maxInputForJump - minInputForJump)));
            onGround = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (currentVelocity >= deathSpeed)
            dead = true;
        if (collision.collider.CompareTag("Ground"))
            onGround = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Goal"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
            onGround = false;
    }
}