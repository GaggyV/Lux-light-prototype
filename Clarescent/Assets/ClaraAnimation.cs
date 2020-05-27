using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClaraAnimation : MonoBehaviour
{
    /*
    Please do this properly later
    */

    private Rigidbody2D rB;
    private Animator animator;
    private ClaraBehavior clara;
    private SoundHandler soundHandler;

    private void Start()
    {
        clara = GetComponent<ClaraBehavior>();
        rB = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        bool walking = Mathf.Abs(rB.velocity.x) > 0.1f;

        animator.SetBool("Walking", walking);
        animator.SetBool("Grabbing", clara.GetState() == ClaraBehavior.State.Grabbing);
        if(clara.GetState() == ClaraBehavior.State.Grabbing)
        {
            soundHandler.CGrabiingSFX();
        }

        animator.SetBool("Moonwalking", transform.localScale.x > 0f && rB.velocity.x < -0.3f || transform.localScale.x < 0f && rB.velocity.x > 0.3f);
    }
}
