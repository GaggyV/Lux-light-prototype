﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class FawnEnemy : MonoBehaviour
{
    [SerializeField] SoundHandler soundHandler;
    public GameObject Clara;

    public float detection;
    public float FawnSpeed;
    public float delayTimer;

    private bool runRight;

    [SerializeField] private float startledTime, runningTime;

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
    }

    // Update is called once per frame
    void Update()
    {
        delayTimer += Time.deltaTime;
        switch (currentState)
        {
            case FawnState.Eating:
                EatingState();
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
                }
                break;
            default:
                break;
        }
    }

    private void StartRunning()
    {
        currentState = FawnState.running;
        Invoke("StopRunning", runningTime);
    }
    private void StopRunning()
    {
        currentState = FawnState.Eating;
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

    private void OnCollisionStay2D(Collision2D collision)
    {
        for(int i = 0; i < collision.contacts.Length; i++)
        {
            if (Mathf.Abs(collision.contacts[i].normal.y) < 0.2f)
            {
                runRight = !runRight;
                break;
            }
        }
    }

}
