using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feet : MonoBehaviour
{
    public bool onGround;
    public static AudioSource jumpSound;

    private void Start()
    {
        jumpSound = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        jumpSound.Play();
        if (!collision.gameObject.CompareTag("Clara")) onGround = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        onGround = true;
    }

}
