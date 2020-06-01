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
        soundHandler = FindObjectOfType<SoundHandler>();
        if(soundHandler == null)
        {
            Debug.LogError("There's no FUCKING sound handler, you muppet");
        }
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
        //animator.SetBool("Climbing", clara.GetState() == ClaraBehavior.State.Climbing); breh
        animator.SetBool("Falling", rB.velocity.magnitude > clara.GetDieSpeed() && !clara.Grounded());
        animator.SetBool("Moonwalking", transform.localScale.x > 0f && rB.velocity.x < -0.3f || transform.localScale.x < 0f && rB.velocity.x > 0.3f);
    }
}
