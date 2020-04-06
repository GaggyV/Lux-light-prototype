using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TingInteraction : MonoBehaviour
{
    public bool canLevitate;
    public bool canBreak;
    public bool canBeScared;
    public Rigidbody2D body;
    public bool broken;
    public bool scared;
    public float scareSpeed;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }
}
