using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feet : MonoBehaviour
{
    public bool onGround;
    [SerializeField] private SoundHandler soundHandler;
    List<Collision2D> collisions = new List<Collision2D>();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Clara")) 
            collisions.Add(collision);
        soundHandler.CLandingSFX();

        if (collision.gameObject.CompareTag("Shrooms"))
        {
            soundHandler.BouncySound();
        }
        if (collisions.Count > 0) onGround = true;
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collisions.Contains(collision)) collisions.Remove(collision);
        if (collisions.Count == 0) onGround = false;
    }

}
