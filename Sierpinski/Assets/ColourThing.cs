using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourThing : MonoBehaviour
{
    private SpriteRenderer sr;
    public Color basecolour;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        sr.color = basecolour / Script.divisor;
    }
}
