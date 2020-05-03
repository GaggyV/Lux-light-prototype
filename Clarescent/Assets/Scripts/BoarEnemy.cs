using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    private bool timedelay;
    float pauseTime = 1f;

    void Start(float WaitforSec)
    {
        WaitforSec = Time.deltaTime + pauseTime;
    }
    bool IsDone()
    {
        return timedelay;
    }
}

public class BoarEnemy : MonoBehaviour
{
    [SerializeField] float boarSpeed;
    [SerializeField] float detectionRange;
    [SerializeField] float patrollingSpeed, chargingSpeed;


    public GameObject boar;
    public GameObject clara;

    float direction;
    Rigidbody2D rb;
    bool FaceRight;

    public Sprite normalWalking;
    public Sprite RedExclamationAlert;
    public Sprite Charge;
    public Sprite Dizzy;


    public enum BoarState
    {
        Charging,
        patrolling,
        Dead
    }

    BoarState currentState = BoarState.patrolling;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //direction = (transform.position + clara.transform.position).normalized * boarSpeed;

        //direction = clara.transform.position.x > transform.position.x ? Vector2.right : -Vector2.right;
        //direction *= boarSpeed;

        //detectionRange = (clara.transform.position - transform.position).magnitude;

        // Finite State Machines (FSM)
        switch (currentState)
        {
            case BoarState.Charging:
                ChargingState();
                break;

            case BoarState.Dead:
                DizzyState();
                gameObject.GetComponent<SpriteRenderer>().sprite = Dizzy;
                break;

            case BoarState.patrolling:
                if (patrollingSpeed == 0)
                {
                    currentState = BoarState.Dead;
                }
                else
                {
                    PatrollingState();
                }
                break;
            default:
                break;
        }
    }
    void IdleState()
    {
        if (detectionRange < 2)
        {
            boar.gameObject.GetComponent<SpriteRenderer>().sprite = RedExclamationAlert;
            currentState = BoarState.Charging;
        }
        else
        {
            currentState = BoarState.patrolling;
        }
    }

    void ChargingState()
    {
        Invoke("Charges", 1.0f);
    }

    void Charges()
    {
        boar.gameObject.GetComponent<SpriteRenderer>().sprite = Charge;
        rb.velocity -= new Vector2(-direction, 0);
    }

    void PatrollingState()
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


    void DizzyState()
    {
        //sphere cast
        rb.velocity = Vector2.zero;
        //transform.position = new Vector2(0, -20);
    }


    bool IsFaceRight()
    {
        return transform.localScale.x > 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
       

        if (collision.gameObject.CompareTag("Crate"))
        {
            collision.gameObject.GetComponent<TingInteraction>().broken = true;
            patrollingSpeed = 0;
            //collision.collider.isTrigger = true;
            return;
        }
        if(Mathf.Abs(collision.contacts[0].normal.x) > 0.9)
            transform.localScale *= new Vector2(-1f, 1f);
    }

}


