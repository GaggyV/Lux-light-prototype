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

    private void Start()
    {
        rB = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        bool walking = Mathf.Abs(rB.velocity.x) > 0.1f;

        animator.SetBool("Walking", walking);
    }
}
