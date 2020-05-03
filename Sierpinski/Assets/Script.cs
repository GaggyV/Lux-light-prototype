using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script : MonoBehaviour
{
    public static float divisor;

    [SerializeField] ColourThing pixel;
    [SerializeField] private Transform[] nodes = new Transform[3];
    [SerializeField] int placedPerFrame;
    SpriteRenderer sr;
    Vector3 currentPos;
    Color color;

    private int pixels = 1000000;
    void Start()
    {
        currentPos = nodes[0].transform.position;
        sr = pixel.GetComponent<SpriteRenderer>();

        for (int i = 0; i < pixels; i++)
        {
            currentPos += (nodes[Random.Range(0, 3)].transform.position - currentPos) / 2;
            color = new Color(1 - (nodes[0].transform.position - currentPos).magnitude / divisor, 1 - (nodes[1].transform.position - currentPos).magnitude / divisor, 1 - (nodes[2].transform.position - currentPos).magnitude / divisor);
            pixel.basecolour = color;
            Instantiate(pixel, currentPos, Quaternion.identity);

        }
    }


    void Update()
    {
       divisor = (Mathf.Sin(Time.time) * 4) + 4;
    }
}
