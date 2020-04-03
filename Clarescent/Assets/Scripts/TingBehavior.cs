using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TingBehavior : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private InputHandler inputHandler;
    
    void Update()
    {
        transform.position += new Vector3(inputHandler.rightStick.x_axis, inputHandler.rightStick.y_axis, 0f) * moveSpeed * Time.deltaTime;
    }
}
