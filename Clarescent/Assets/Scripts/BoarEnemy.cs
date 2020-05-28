using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoarEnemy : MonoBehaviour
{
    [SerializeField] float boarSpeed;
    [SerializeField] float detectionRange;
    [SerializeField] float patrollingSpeed, chargingSpeed;

    public GameObject clara;

    float direction;
    float pauseTime = 1f;
    float timewait;
    Rigidbody2D rb;

    public enum BoarState
    {
        Charging,
        patrolling,
        Dead
    }

    BoarState currentState = BoarState.patrolling;

  
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }


    void Update()
    {
        timewait = Time.deltaTime + pauseTime; 
        //direction = (transform.position + clara.transform.position).normalized * boarSpeed;

        //direction = clara.transform.position.x > transform.position.x ? Vector2.right : -Vector2.right;

        //detectionRange = (clara.transform.position - transform.position).magnitude;

        // Finite State Machines (FSM)
        switch (currentState)
        {
            case BoarState.patrolling:
                PatrollingState();
                break;

            case BoarState.Charging:
                ChargingState();
                break;

            case BoarState.Dead:
                DizzyState();
                break;

            default:
                break;
        }
    }
    void IdleState()
    {
        if (detectionRange < 2)
        {
            currentState = BoarState.Charging;
        }
        else
        {
            currentState = BoarState.patrolling;
        }
    }

    void ChargingState()
    {
        rb.velocity -= new Vector2(-direction, 0);
    }



    void PatrollingState()
    {
        if (IsFaceLeft())
        {
            rb.velocity = new Vector2(patrollingSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-patrollingSpeed, rb.velocity.y);
        }

        if (patrollingSpeed == 0)
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
    }


    bool IsFaceLeft()
    {
        return transform.localScale.x < 0;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
       

        if (collision.gameObject.CompareTag("Crate"))
        {
            collision.gameObject.GetComponent<TingInteraction>().broken = true;
            patrollingSpeed = 0 + timewait;
           
            return;
        }
        //if(Mathf.Abs(collision.contacts[0].normal.x) > 0.9)
            //transform.localScale *= new Vector2(-1f, 1f);
    }

}


