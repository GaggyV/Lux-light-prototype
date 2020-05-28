using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaredEnemy : MonoBehaviour
{
    Vector2 direction;
    [SerializeField] float detectionRange;
    [SerializeField] float speed;
    public GameObject clara;

    public Vector3 scale;

    Rigidbody2D rb;
    bool facingLeft = false;
    [SerializeField] private SoundHandler soundHandler;


    void Start()
    {
        clara = FindObjectOfType<ClaraBehavior>().gameObject;
        rb = GetComponent<Rigidbody2D>();
        soundHandler = FindObjectOfType<SoundHandler>();
    }
    void Update()
    {
        //detectionRange = (clara.transform.position - transform.position).magnitude;
        if (clara == null) return;
            direction = (transform.position - clara.transform.position).normalized * speed;

        if ((clara.transform.position - transform.position).magnitude < detectionRange)
        {
            soundHandler.FawnRunningSFX();
            rb.velocity += new Vector2(direction.x, 0);
            if(facingLeft )
            {
            facingLeft = true;
            transform.localScale *= new Vector2(-1f, 1f);
            }

        }

        else
        {
            rb.velocity = new Vector2(rb.velocity.x * 0.8f , rb.velocity.y);
        }

    }

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ground")
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-1 * scale.x, 0);
            scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Clara")
        {
            Invoke("Death", 0.5f);
        }
    }

    void Death()
    {
        Destroy(gameObject);
        soundHandler.FawnStartledSFX();
    }
       
}
  
