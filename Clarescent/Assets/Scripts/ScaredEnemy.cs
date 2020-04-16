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
    public Sprite Walking;
    public Sprite Dead;
    //public Animator animator;
    Rigidbody2D rb;
    

    void Start()
    {
        //clara = GameObject.FindGameObjectWithTag("Clara");
        rb = enemy.GetComponent<Rigidbody2D>();

    }
    void Update()
    {
        detectionRange = (clara.transform.position - transform.position).magnitude;
        direction = (transform.position - clara.transform.position).normalized * speed;

        if (detectionRange < 2)
        {
            rb.velocity += new Vector2(direction.x, 0);
            enemy.gameObject.GetComponent<SpriteRenderer>().sprite = Walking;
            //animator.SetFloat("speed", Mathf.Abs(speed)); 
        }
    }











   /* private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Clara")
        {
            Debug.Log("Nani");
            this.gameObject.GetComponent<SpriteRenderer>().sprite = Dead;
            //Destroy(rb);
            transform.position = new Vector2(0, -200).normalized;
            Destroy(this.gameObject);
            //animator.SetBool("isdead", true);
        }
    }*/

}
        /*
            enemy.GetComponent<Rigidbody2D>().velocity = (new Vector2(direction.x , direction.y));
     */
