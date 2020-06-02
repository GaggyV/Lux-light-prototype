using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHack : MonoBehaviour
{
    private ClaraBehavior clara;

    private void Start()
    {
        clara = GetComponentInParent<ClaraBehavior>();
    }

    public void StopClimb()
    {
        clara.EndClimb();
    }
}
