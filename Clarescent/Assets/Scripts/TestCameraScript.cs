using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCameraScript : MonoBehaviour
{
    private Transform player;

    void Start()
    {
        player = FindObjectOfType<ClaraBehavior>().transform;
    }

    
    void Update()
    {
        
    }
}
