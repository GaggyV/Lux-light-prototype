using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feet : MonoBehaviour
{
    public bool onGround;
    [SerializeField] private SoundHandler soundHandler;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Clara")) onGround = true;
        soundHandler.CLandingSFX();

        if (collision.gameObject.CompareTag("Shrooms"))
        {
            soundHandler.BouncySound();
        }
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        onGround = false;
    }

}
