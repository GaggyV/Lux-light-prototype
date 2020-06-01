using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCamera : MonoBehaviour
{
    public List<Transform> targets;
    public Vector3 offset;
    private Vector3 velocity;
    public float smoothness;
    public float minZoom = 40f;
    public float maxZoom = 10f;
    public float zooming = 50f;
    public Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        if(targets.Count == 0)
        {
            return; 
        }
        Vector3 center = Getcenter();
        Vector3 newPosition = center + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothness);

        
    }

    void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetBtwWidth() / zooming);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
    }

    float GetBtwWidth()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.size.x;
    }

    Vector3 Getcenter()
    {
        if(targets.Count == 1)
        {
            return targets[0].position;
        }

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            if(targets[i].gameObject.activeInHierarchy)
                bounds.Encapsulate(targets[i].position);
        }
        return bounds.center;
    }
}
