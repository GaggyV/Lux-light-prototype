using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClaraInteractable : MonoBehaviour
{
    public Rigidbody2D body;
    public bool colliding;
    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }
}
