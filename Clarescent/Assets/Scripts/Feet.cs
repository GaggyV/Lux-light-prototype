using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feet : MonoBehaviour
{
    public bool onGround;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Clara")) onGround = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        onGround = true;
    }
}
