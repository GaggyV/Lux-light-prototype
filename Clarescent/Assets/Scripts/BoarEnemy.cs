using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*Boar breaks boxes when charging into them, and then stops charging (becomes dazed)
Boar will patrol when not charging*/

public class BoarEnemy : MonoBehaviour
{
    [SerializeField] float boarSpeed;
    [SerializeField] float detectionRange;
    [SerializeField] float patrollingSpeed;
    [SerializeField] float waitForSeconds;
    //public GameObject crushedBox;
    public GameObject boar;
    public GameObject clara;
    //public GameObject box1;
    Vector2 direction;
    Rigidbody2D rb;
    bool FaceRight;

    public Sprite normalWalking;
    public Sprite RedExclamationAlert;
    public Sprite Charge;
    public Sprite Dizzy;
    public Sprite brokenSprite;
    bool spriteChange = false;

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

            //StopCoroutine(myRoutine());

        }


        else
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
        }
    }


    IEnumerator myRoutine()
    {
        boar.gameObject.GetComponent<SpriteRenderer>().sprite = RedExclamationAlert;

        yield return new WaitForSeconds(waitForSeconds);

        boar.gameObject.GetComponent<SpriteRenderer>().sprite = Charge;

        rb.velocity -= new Vector2(-direction.x, 0);

        //yield return new WaitForSeconds(1/2);
        //rb.velocity = new Vector2(0, 0);
    }


    bool IsFaceRight()
    {
        return transform.localScale.x > 0;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        transform.localScale *= new Vector2(-1f, 1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Crate"))
        {
            collision.gameObject.GetComponent<TingInteraction>().broken = true;
            //collision.collider.isTrigger = true;
            if (gameObject.GetComponent<SpriteRenderer>().sprite = brokenSprite)
            {
                boar.gameObject.GetComponent<SpriteRenderer>().sprite = Dizzy;
            }
                
        }
    }

}