using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrooms : MonoBehaviour
{
    [SerializeField] GameObject Clara;
    [SerializeField] float ThrustUp;

    /*private void OnCollisionEnter2D(Collision2D collision)
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
    }*/

    /* void OnCollisionEnter(Collision collision)
     {
         if (collision.Tag == "Clara")
             Debug.Log("OnCollisionEnter called");

             Debug.Log("It was the player");
             //var rb = transform.GetComponentInChildren<Rigidbody2D>();
             //rb.velocity = transform.up * ThrustUp;
     }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Clara")
        {
            Debug.Log("It was the player");
            //GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }

    }
}