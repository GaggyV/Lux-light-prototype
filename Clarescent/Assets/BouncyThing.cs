using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyThing : MonoBehaviour
{

    [SerializeField] private float force;
    [SerializeField] SoundHandler SoundHandler;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var rb = collision.transform.GetComponentInParent<Rigidbody2D>();
        if (rb != null)
        {
            SoundHandler.BouncySound();
            rb.AddForce((Vector2)transform.up * force);        
        }
    }
}
