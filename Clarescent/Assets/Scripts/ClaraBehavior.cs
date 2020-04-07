using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ClaraBehavior : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float speedCap;
    [SerializeField] private float maxJumpStrength;
    [SerializeField] private InputHandler inputHandler;
    private bool onGround;
    private Rigidbody2D rb;
    [SerializeField] private float currentVelocity;
    [SerializeField] private float deathSpeed;
    private bool dead;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (dead) return;
        if (inputHandler.leftStick.x_axis != 0f || inputHandler.leftStick.y_axis != 0f)
            rb.velocity += new Vector2(inputHandler.leftStick.x_axis * movementSpeed * Time.deltaTime, 0f);
        if (rb.velocity.x > speedCap) rb.velocity = new Vector2(speedCap, rb.velocity.y);
        if (rb.velocity.x < -speedCap) rb.velocity = new Vector2(-speedCap, rb.velocity.y);
        if (inputHandler.leftTriggerAnalog.axis != -1f && onGround)
            rb.velocity = new Vector2(rb.velocity.x, maxJumpStrength * inputHandler.leftTriggerAnalog.axis);

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