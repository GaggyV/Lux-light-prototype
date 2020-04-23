using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    private bool timedelay;

    void Start(float WaitforSec)
    {
        WaitforSec = Time.deltaTime;
    }
    bool IsDone()
    {
        return timedelay;
    }
}

public class BoarEnemy : MonoBehaviour
{
    public enum BoarState
    {
        Idle,
        Charging,
        patrolling,
        Dead
    }
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

    BoarState currentState = BoarState.Idle;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void IdleState()
    {
        detectionRange = (clara.transform.position - transform.position).magnitude;
        if (detectionRange < 2)
        {
            currentState = BoarState.Charging;
        }
    }

    void ChargingState()
    {
        boar.gameObject.GetComponent<SpriteRenderer>().sprite = RedExclamationAlert;
        Invoke("p", 1.0f);
        //(waitForSeconds);
        boar.gameObject.GetComponent<SpriteRenderer>().sprite = Charge;
        rb.velocity -= new Vector2(-direction.x, 0);
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
        //(patrollingSpeed == 0)
        Destroy(GameObject.Find("Crate"));
        //sphere cast
        gameObject.GetComponent<SpriteRenderer>().sprite = Dizzy;
        rb.velocity = new Vector2(0, 0);
        //yield return new WaitForSeconds(2);
        transform.position = new Vector2(0, -20);
    }

    void Update()
    {
        direction = (transform.position + clara.transform.position).normalized * boarSpeed;

        // Finite State Machines (FSM)
        switch (currentState)
        {
            case BoarState.Idle: IdleState(); break;
            
            case BoarState.Charging: ChargingState(); break;

            case BoarState.patrolling: PatrollingState(); break;

            default: break;
          
        }

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
            patrollingSpeed = 0;
            //collision.collider.isTrigger = true;
        }
    }

}
