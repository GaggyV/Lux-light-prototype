using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class FawnEnemy : MonoBehaviour
{
    [SerializeField] SoundHandler soundHandler;
    public GameObject Clara;
    private Animator animator;

    public float detection;
    public float FawnSpeed;
    public float delayTimer;

    private bool doNothing;

    private bool runRight;

    [SerializeField] private float startledTime, runningTime, doNothingTime;

    private float lastDoNothingTime = -999999f;

    Rigidbody2D Rb;

    public enum FawnState
    {
        Eating,
        Startled,
        running,
        Dead
    }

    FawnState currentState = FawnState.Eating;

    // Start is called before the first frame update
    void Start()
    {
        Clara = FindObjectOfType<ClaraBehavior>().gameObject;
        Rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time < lastDoNothingTime + doNothingTime) return;

        
        switch (currentState)
        {
            case FawnState.Eating:
                EatingState();
                soundHandler.FawnEatingSFX();
                break;
            case FawnState.Startled:
                break;
            case FawnState.running:
                RunningState();
                break;
            case FawnState.Dead:
                break;
            default:
                break;
        }
        UpdateState();
    }

    private void UpdateState()
    {
        switch(currentState)
        {
            case FawnState.Eating:
                if ((Clara.transform.position - transform.position).magnitude < detection)
                {
                    print("I'm am scared");
                    Invoke("StartRunning", startledTime);
                    currentState = FawnState.Startled;
                    runRight = Clara.transform.position.x < transform.position.x;
                    animator.SetBool("Running", true);
                }
                break;
            default:
                break;
        }
    }

    private void StartRunning()
    {
        currentState = FawnState.running;
        soundHandler.FawnRunningSFX();
        Invoke("StopRunning", runningTime);
    }
    private void StopRunning()
    {
        soundHandler.FawnStartledSFX();
        currentState = FawnState.Eating;
        animator.SetBool("Running", false);
    }

    void EatingState()
    {
        //do nothing
    }

    void StartledState()
    {

    }

    void RunningState()
    {
        if ((Clara.transform.position - transform.position).magnitude < detection)
        {
            runRight = Clara.transform.position.x < transform.position.x;
        }

        float multiplier = runRight ? 1f : -1f;
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -multiplier, transform.localScale.y, 1f);
        Rb.velocity = new Vector2(FawnSpeed * multiplier, Rb.velocity.y);
    }

    void DeadState()
    {

    }


    private void SetRunning()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.GetComponent<ClaraBehavior>() != null)
        {
            doNothing = true;
            lastDoNothingTime = Time.time;
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        for(int i = 0; i < collision.contacts.Length; i++)
        {
            if (collision.transform.CompareTag("Crate")) return; //bruh
            if (Mathf.Abs(collision.contacts[i].normal.y) < 0.2f)
            {
                runRight = !runRight;
                break;
            }
        }
    }

}
