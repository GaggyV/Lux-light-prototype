using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaredEnemy : MonoBehaviour
{
    Vector2 direction;
    Vector2 run;
    [SerializeField] float detectionRange;
    [SerializeField] float speed;
    public GameObject enemy;
    public GameObject clara;
    public Animator animator;
    Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        //clara = GameObject.FindGameObjectWithTag("Clara");
        rb = GetComponent<Rigidbody2D>();
        run = transform.position; 
    }

    // Update is called once per frame
    void Update()
    {
        detectionRange = ((clara.transform.position) - (transform.position)).magnitude;
        direction = ((transform.position) - (enemy.transform.position)).normalized * speed;

        if (detectionRange > 20)
        {
            enemy.GetComponent<Rigidbody2D>().velocity = (new Vector2(direction.x , direction.y));
            animator.SetFloat("speed", Mathf.Abs(speed)); 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if  (collision.gameObject)
        {
            Destroy(enemy);
            animator.SetBool("isdead", true);
        }
    }
}
