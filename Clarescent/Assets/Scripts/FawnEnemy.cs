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
                break;
            case FawnState.Dead:
                break;
            default:
                break;
        }
    }

    void EatingState()
    {

    }

    void StartledState()
    {

    }

    void RunningState()
    {

    }

    void DeadState()
    {

    }

}
