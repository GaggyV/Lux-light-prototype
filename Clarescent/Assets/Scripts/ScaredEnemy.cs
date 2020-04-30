using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaredEnemy : MonoBehaviour
{
    Vector2 direction;
    [SerializeField] float detectionRange;
    [SerializeField] float speed;
    //[SerializeField] float runAwayDistance;
    public GameObject clara;
    public GameObject enemy;
    public Sprite ScarednRunning;
    public Sprite Sleeping;
    public Sprite Dead;
    public Vector2 yoRB;
    public Vector3 scale;

    //public Animator animator;
    Rigidbody2D rb;

    Bounds collisionBounds = new Bounds();

    void Start()
    {
        rb = enemy.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        detectionRange = (clara.transform.position - transform.position).magnitude;
        direction = (transform.position - clara.transform.position).normalized * speed;
        yoRB = GetComponent<Rigidbody2D>().velocity;

        if (detectionRange < 2)
        {
            rb.velocity += new Vector2(direction.x, 0);
            enemy.gameObject.GetComponent<SpriteRenderer>().sprite = ScarednRunning;
            //animator.SetFloat("speed", Mathf.Abs(speed)); 
        }
        else
        {
            enemy.gameObject.GetComponent<SpriteRenderer>().sprite = Sleeping;
        }
        if (detectionRange <1)
        {
            rb.velocity = new Vector2(0, 0);
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
            transform.position = new Vector2(0, -10).normalized;
            IsDead();
            Invoke("Death", 1f);
            
        }
    }

    bool IsDead()
    {
        return enemy.gameObject.GetComponent<SpriteRenderer>().sprite = Dead;
    }

    void Death()
    {
        Destroy(gameObject);
    }
       
}
  
