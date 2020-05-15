using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrooms : MonoBehaviour
{
    [SerializeField] float ThrustUp;

    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Clara"))
        {
            Debug.Log("fikidi fak");
        }
        var rb = collision.transform.GetComponentInChildren<Rigidbody2D>();
        if(rb != null)
        {
            rb.AddForce(transform.up * ThrustUp);
        }
    }
}
