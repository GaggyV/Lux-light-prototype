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
    [SerializeField] private SoundHandler soundHandler;
    [SerializeField] private Feet feet;
    [SerializeField] private bool GodMode;
    [SerializeField] Grid grid;
    private Vector3 climbingDest;
    private enum State { walking, climbing, jumping };
    private State currentState;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //TODO
        //rb.sharedMaterial.friction = horizontalFriction;
        //rb.drag = drag;
        minJumpSqrt = Mathf.Sqrt(minJumpHeight);
        maxJumpSqrt = Mathf.Sqrt(maxJumpHeight);
        gravityCoEf = Mathf.Sqrt(Physics2D.gravity.magnitude) * Mathf.Sqrt(2f);
        grid = FindObjectOfType<Grid>();
    }
    void Update()
    {

        if (dead) return;
        switch (currentState)
        {
            case State.walking:
                if (inputHandler.leftStick.x_axis != 0f)
                {
                    rb.velocity += new Vector2(inputHandler.leftStick.x_axis * horizontalAcceleration * Time.deltaTime, 0f);
                }
                if (rb.velocity.x > maximumHorizontalSpeed) rb.velocity = new Vector2(maximumHorizontalSpeed, rb.velocity.y);
                if (rb.velocity.x < -maximumHorizontalSpeed) rb.velocity = new Vector2(-maximumHorizontalSpeed, rb.velocity.y);

                if (rb.velocity.x > 0.1f && transform.localScale.x < 0 || rb.velocity.x < -0.1f && transform.localScale.x > 0)
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

                if (inputHandler.leftTriggerAnalog.axis >= maxInputForJump && feet.onGround)
                {
                    rb.velocity = new Vector2(rb.velocity.x, gravityCoEf * maxJumpSqrt);
                    feet.onGround = false;
                    soundHandler.Jump();
                }
                else if (inputHandler.leftTriggerAnalog.axis >= minInputForJump && feet.onGround)
                {
                    rb.velocity = new Vector2(rb.velocity.x, gravityCoEf * minJumpSqrt + gravityCoEf * ((maxJumpSqrt - minJumpSqrt) / maxJumpSqrt) * ((inputHandler.leftTriggerAnalog.axis - minInputForJump) / (maxInputForJump - minInputForJump)));
                    feet.onGround = false;
                    soundHandler.Jump();
                }
                if (inputHandler.leftTriggerDigital.enter && inputHandler.leftStick.x_axis == 0f)
                {
                    if (AbleToClimb())
                    {
                        currentState = State.climbing;
                        climbingDest = new Vector3(Mathf.Floor(transform.position.x) + (transform.localScale.x > 0f ? 1.5f : -0.5f),
                            Mathf.Floor(transform.position.y) + 2.5f);
                        rb.isKinematic = true;
                    }
                }
                break;
            case State.climbing:
                if ((transform.position - climbingDest).sqrMagnitude > 0.1)
                    transform.position = Vector3.Lerp(transform.position, climbingDest, 0.1f);
                else
                {
                    rb.isKinematic = false;
                    currentState = State.walking;
                }
                break;
        }
        //soundHandler.walking = inputHandler.leftStick.x_axis != 0f && onGround;
                if (inputHandler.leftStick.x_axis != 0f && onGround)
                {
                    soundHandler.ClaraWalkSFX();
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

    private bool AbleToClimb()
    {
        Vector2 checkPos;
        checkPos.x = Mathf.Floor(transform.position.x) + 0.5f;
        checkPos.y = Mathf.Floor(transform.position.y) + 0.5f;
        RaycastHit2D hit = Physics2D.Raycast(checkPos + Vector2.right * (transform.localScale.x > 0 ? 1 : -1), Vector2.zero);
        if (!hit) return false;
        hit = Physics2D.Raycast(checkPos + Vector2.up * 2, Vector2.zero);
        if (hit) return false;
        hit = Physics2D.Raycast(checkPos + Vector2.right * (transform.localScale.x > 0 ? 1 : -1) + Vector2.up, Vector2.zero);
        if (hit) return false;
        hit = Physics2D.Raycast(checkPos + Vector2.right * (transform.localScale.x > 0 ? 1 : -1) + Vector2.up * 2, Vector2.zero);
        if (hit) return false;
        return true;
    }

    private void OnDrawGizmos()
    {

        Vector2 checkPos;
        checkPos.x = Mathf.Floor(transform.position.x) + 0.5f;
        checkPos.y = Mathf.Floor(transform.position.y) + 0.5f;
        Gizmos.color = Color.white;

        Gizmos.DrawCube(checkPos + Vector2.up * 2, Vector3.one / 10);
        Gizmos.DrawCube(checkPos + Vector2.right * (transform.localScale.x > 0 ? 1 : -1), Vector3.one / 10);
        Gizmos.DrawCube(checkPos + Vector2.right * (transform.localScale.x > 0 ? 1 : -1) + Vector2.up, Vector3.one / 10);
        Gizmos.DrawCube(checkPos + Vector2.right * (transform.localScale.x > 0 ? 1 : -1) + Vector2.up * 2, Vector3.one / 10);


    }

}