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
    [SerializeField] private Hands hands;
    [SerializeField] private bool GodMode;
    [SerializeField] Grid grid;
    private Vector3 interactorOffset;
    private Vector3 climbingDest;
    private bool onGroundLagger;
    internal enum State { Walking, Climbing, Jumping, Grabbing };
    private State currentState;


    private bool midAnimation;
    private Transform gfxTransform;

    Camera cam;

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

        gfxTransform = GetComponentInChildren<Animator>().transform;
    }

    public float GetDieSpeed()
    {
        return deathSpeed;
    }


    public bool Grounded()
    {
        return onGround;
    }

    void Update()
    {
        if(midAnimation)
        {
            transform.position = new Vector3(climbingDest.x, transform.position.y, 0f);
            return;
        }
        if (dead) return;
        if (feet.onGround && !onGroundLagger && currentVelocity >= deathSpeed)
            dead = true;
        onGroundLagger = feet.onGround;
        switch (currentState)
        {
            case State.Walking:
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
                    currentState = State.Jumping;
                }
                else if (inputHandler.leftTriggerAnalog.axis >= minInputForJump && feet.onGround)
                {
                    rb.velocity = new Vector2(rb.velocity.x, gravityCoEf * minJumpSqrt + gravityCoEf * ((maxJumpSqrt - minJumpSqrt) / maxJumpSqrt) * ((inputHandler.leftTriggerAnalog.axis - minInputForJump) / (maxInputForJump - minInputForJump)));
                    feet.onGround = false;
                    soundHandler.Jump();
                    currentState = State.Jumping;
                }
                if (inputHandler.grab.enter)
                {
                   
                    if (hands.interactor != null)
                    {
                        interactorOffset = hands.interactor.transform.position - hands.transform.position;
                        hands.interactor.body.constraints = RigidbodyConstraints2D.FreezeRotation;
                        soundHandler.GrabbingLoopSFX();
                        currentState = State.Grabbing;
                    }
                    
                }
                break;
            case State.Jumping:

                if (feet.onGround) currentState = State.Walking;
                if (AbleToClimb())
                {
                    rb.velocity = Vector2.zero;
                    currentState = State.Climbing;
                    climbingDest = new Vector3(Mathf.Floor(transform.position.x) + (transform.localScale.x > 0f ? 1.3f : -0.3f),
                        Mathf.Floor(transform.position.y) + 2.5f);
                    StartClimb();
                    //rb.isKinematic = true;







                    soundHandler.ClaraClimbingSFX();
                }
                if (inputHandler.leftStick.x_axis != 0f)
                {
                    rb.velocity += new Vector2(inputHandler.leftStick.x_axis * horizontalAcceleration * Time.deltaTime, 0f);
                }
                if (rb.velocity.x > maximumHorizontalSpeed) rb.velocity = new Vector2(maximumHorizontalSpeed, rb.velocity.y);
                if (rb.velocity.x < -maximumHorizontalSpeed) rb.velocity = new Vector2(-maximumHorizontalSpeed, rb.velocity.y);

                if (rb.velocity.x > 0.1f && transform.localScale.x < 0 || rb.velocity.x < -0.1f && transform.localScale.x > 0)
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                break;
            case State.Climbing:
                //if ((transform.position - climbingDest).magnitude > 0.6f)
                //    transform.position = Vector3.Lerp(transform.position, climbingDest, 0.1f);
                //else
                //{
                //    rb.isKinematic = false;
                //    currentState = State.Walking;
                //}
 
                transform.position = climbingDest;
                midAnimation = true;

                //rb.isKinematic = false;
                //currentState = State.Walking;

                break;
            case State.Grabbing:
                if (hands.interactor == null)
                {
                    currentState = State.Walking;
                    soundHandler.GrabStopSFX();
                }
                if (!inputHandler.grab.held)
                {
                    hands.interactor.body.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
                    currentState = State.Walking;
                    soundHandler.GrabStopSFX();
                }
                if (inputHandler.leftStick.x_axis != 0f)
                {
                    rb.velocity += new Vector2(inputHandler.leftStick.x_axis * horizontalAcceleration * Time.deltaTime, 0f);
                }
                if (rb.velocity.x > maximumHorizontalSpeed) rb.velocity = new Vector2(maximumHorizontalSpeed, rb.velocity.y);
                if (rb.velocity.x < -maximumHorizontalSpeed) rb.velocity = new Vector2(-maximumHorizontalSpeed, rb.velocity.y);

                if (hands.interactor != null)
                    hands.interactor.body.velocity = new Vector2(rb.velocity.x, hands.interactor.body.velocity.y);
               

                break;
        }
        currentVelocity = rb.velocity.magnitude;
        //soundHandler.walking = inputHandler.leftStick.x_axis != 0f && onGround;
                if (inputHandler.leftStick.x_axis != 0f && onGround)
                {
                    soundHandler.ClaraWalkSFX();
                }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        //if (collision.collider.CompareTag("Ground"))
        //    onGround = true;
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
        //if (collision.collider.CompareTag("Ground"))
        //    onGround = false;
    }

    private bool AbleToClimb()
    {
        Vector2 checkPos = (Vector2)transform.position;
        RaycastHit2D hit = Physics2D.Raycast(checkPos + Vector2.right * (transform.localScale.x > 0 ? 1 : -1), Vector2.zero);
        if (!hit || hit.collider.CompareTag("Ting")) return false;
        hit = Physics2D.Raycast(checkPos + Vector2.up * 2, Vector2.zero);
        if (hit && !hit.collider.CompareTag("Ting")) return false;
        hit = Physics2D.Raycast(checkPos + Vector2.right * (transform.localScale.x > 0 ? 1 : -1) + Vector2.up, Vector2.zero);
        if (hit && !hit.collider.CompareTag("Ting")) return false;
        hit = Physics2D.Raycast(checkPos + Vector2.right * (transform.localScale.x > 0 ? 1 : -1) + Vector2.up * 2, Vector2.zero);
        if (hit && !hit.collider.CompareTag("Ting")) return false;
        hit = Physics2D.Raycast(checkPos + Vector2.right * (transform.localScale.x > 0 ? 1 : -1) + Vector2.up * 2.5f, Vector2.zero);
        if (hit && !hit.collider.CompareTag("Ting")) return false;
        return true;
    }

    internal State GetState()
    {
        return currentState;
    }

    public void StartClimb()
    {
        transform.position = climbingDest;
        float multiplier = transform.localScale.x > 0f ? 1f : 1f;
        gfxTransform.localPosition = new Vector3(-1.5f * multiplier, -3.24f - 2.45f, 0f);
    }

    public void EndClimb()
    {
        gfxTransform.localPosition = new Vector3(0f, -2.45f, 0f);
        currentState = State.Walking;
        midAnimation = false;
    }

    private void OnDrawGizmos()
    {

        Vector2 checkPos;
        checkPos = new Vector2(transform.position.x, transform.position.y);
        Gizmos.color = Color.white;

        Gizmos.DrawCube(checkPos + Vector2.up * 2, Vector3.one / 10);
        Gizmos.DrawCube(checkPos + Vector2.right * (transform.localScale.x > 0 ? 1 : -1), Vector3.one / 10);
        Gizmos.DrawCube(checkPos + Vector2.right * (transform.localScale.x > 0 ? 1 : -1) + Vector2.up, Vector3.one / 10);
        Gizmos.DrawCube(checkPos + Vector2.right * (transform.localScale.x > 0 ? 1 : -1) + Vector2.up * 2, Vector3.one / 10);
        Gizmos.DrawCube(checkPos + Vector2.right * (transform.localScale.x > 0 ? 1 : -1) + Vector2.up * 2.5f, Vector3.one / 10);

    }

}