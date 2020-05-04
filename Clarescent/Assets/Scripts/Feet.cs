using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feet : MonoBehaviour
{
    public bool onGround;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.CompareTag("Clara")) onGround = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        onGround = true;
    }
    
}
