using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BoarEnemy : MonoBehaviour
{
    [SerializeField] float boarSpeed;
    [SerializeField] float detectionRange;
    [SerializeField] float patrollingSpeed;
    [SerializeField] float waitForSeconds;

    public GameObject boar;
    public GameObject clara;

    Vector2 direction;
    Rigidbody2D rb;
    bool FaceRight;

    public Sprite normalWalking;
    public Sprite RedExclamationAlert;
    public Sprite Charge;
    public Sprite Dizzy;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        detectionRange = (clara.transform.position - transform.position).magnitude;
        direction = (transform.position + clara.transform.position).normalized * boarSpeed;

        if (detectionRange < 2)
        {
            StartCoroutine(myRoutine());
        }

        else
        {
            StartCoroutine(patrolling());
        }

        if (patrollingSpeed == 0)
        {
            Destroy(GameObject.Find("Crate"));
            StartCoroutine(dizziness());
        }

    }

    IEnumerator myRoutine()
    {
        boar.gameObject.GetComponent<SpriteRenderer>().sprite = RedExclamationAlert;

        yield return new WaitForSeconds(2);

        boar.gameObject.GetComponent<SpriteRenderer>().sprite = Charge;

        rb.velocity -= new Vector2( -direction.x, 0);
    }

    IEnumerator patrolling()
    {
        boar.gameObject.GetComponent<SpriteRenderer>().sprite = normalWalking;
        if (IsFaceRight())
        {
            rb.velocity = new Vector2(patrollingSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-patrollingSpeed, rb.velocity.y);
        }
        yield return null;
    }

    bool IsFaceRight()
    {
        return transform.localScale.x > 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        transform.localScale *= new Vector2(-1f, 1f);
    }


    IEnumerator dizziness()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = Dizzy;
        rb.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(2);
        transform.position = new Vector2(0, -20);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Crate"))
        {
            collision.gameObject.GetComponent<TingInteraction>().broken = true;
            patrollingSpeed = 0;
            //collision.collider.isTrigger = true;
        }
    }

}

