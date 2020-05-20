using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScript : MonoBehaviour
{
    [SerializeField] private float padding;
    private float length, startpos;
    private GameObject cam;
    public float parallaxEffect;

    private bool clone = false;

    [SerializeField] private bool looping = true, fixedPosition;

    private float xOffset = 0;

    private void Start()
    {

        cam = Camera.main.gameObject;
        if(fixedPosition)
        {
            //float temp = (cam.transform.position.x * (1 - parallaxEffect));
            //float dist = (cam.transform.position.x * parallaxEffect);
            startpos = transform.position.x;
            xOffset = transform.position.x; //- cam.transform.position.x;
            //float newX = player.position.x + xDist / (1.0f + parallaxEffect);
            //transform.position = new Vector3(newX, transform.position.y, 0f);
            //startpos = transform.position.x
        }

        var sr = GetComponent<SpriteRenderer>();

        {
            List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();
            if (sr != null) spriteRenderers.Add(sr);
            var childRenderers = GetComponentsInChildren<SpriteRenderer>();

            for(int i = 0; i < childRenderers.Length; i++)
            {
                spriteRenderers.Add(childRenderers[i]);
            }
            
            if(spriteRenderers.Count < 1)
            {
                Debug.LogError("You've added a parallax to a thing which has no sprite renderer and has no children with sprite renderes, stupid.");
                return;
            }
            float minx = 1000f, maxx = -1000f;
            foreach(var sprite in spriteRenderers)
            {
                minx = Mathf.Min((sprite.transform.position.x - sprite.bounds.extents.x) - transform.position.x, minx);
                maxx = Mathf.Max((sprite.transform.position.x + sprite.bounds.extents.x) - transform.position.x, maxx);
            }
            length = maxx - minx;
            print(length);
            length += padding;
        }
        if (clone || !looping) return;
        var cloneObj = Instantiate(this, transform.position + Vector3.right * length, transform.rotation);
        cloneObj.clone = true;
    }

    private void Update()
    {
        //transform.position = new Vector3(cam.transform.position.x, transform.position.y, transform.position.z);

        float temp = ((cam.transform.position.x) * (1 - parallaxEffect));
        float dist = ((cam.transform.position.x - xOffset) * parallaxEffect);

        transform.position = new Vector3((startpos) + dist, transform.position.y, transform.position.z);

        if (!looping) return;
        if (temp > startpos + length) startpos += length * 2;
        else if (temp < startpos - length) startpos -= length * 2;
    }
}
