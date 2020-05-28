using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] InputHandler inputHandler;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
                Time.timeScale = 0;
            else if (Time.timeScale == 0)
                Time.timeScale = 1;
        }

    }
}
