using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaredEnemy : MonoBehaviour
{
    Vector2 direction;
    [SerializeField] float detectionRange;
    [SerializeField] float speed;
    public GameObject clara;
    //public Animator animator;
    Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        //clara = GameObject.FindGameObjectWithTag("Clara");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        detectionRange = (clara.transform.position - transform.position).magnitude;
        direction = (transform.position - clara.transform.position).normalized * speed;

        if (detectionRange < 3)
        {
            rb.velocity += new Vector2(direction.x, 0);
            //animator.SetFloat("speed", Mathf.Abs(speed)); 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if  (collision.gameObject.name == "Clara")
        {
            Destroy(this);
            //animator.SetBool("isdead", true);
        }
    }
}
