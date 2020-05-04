using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScript : MonoBehaviour
{

    private float length, startpos;
    private GameObject cam;
    public float parallaxEffect;
    private GameObject player;

    private void Start()
    {
        cam = Camera.main.gameObject;
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        player = FindObjectOfType<ClaraBehavior>().gameObject;
    }

    private void Update()
    {
        transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);

        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);

      transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

       //f (temp > startpos + length) startpos += length;
       //lse if (temp < startpos - length) startpos -= length;
    }
}
