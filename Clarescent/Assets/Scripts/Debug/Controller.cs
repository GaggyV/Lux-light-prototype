using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Custom
{
    public class Controller : MonoBehaviour
    {
        Rigidbody2D rb;
        [SerializeField]
        float forceMultiplier;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }


        private void Update()
        {
           // Vector2 forceVector = new Vector2(Input.GetAxis("LeftStickHorizontal"), Input.GetAxis("LeftStickHorizontal"));
           // transform.Translate(forceVector*Time.deltaTime*forceMultiplier);
            if (Input.GetKeyDown(KeyCode.JoystickButton0)) { Debug.Log("JoystickButton0"); }
            if (Input.GetKeyDown(KeyCode.JoystickButton1)) { Debug.Log("JoystickButton1"); }
 
        }

        private void FixedUpdate()
        {
           // Vector2 forceVector = new Vector2(Input.GetAxis("LeftStickHorizontal"), Input.GetAxis("LeftStickHorizontal"));
           // Debug.Log("yoo");
           // rb.AddForce(forceVector * forceMultiplier);
            if (Input.GetKeyDown(KeyCode.JoystickButton0)) { Debug.Log("JoystickButton0"); }
            if (Input.GetKeyDown(KeyCode.JoystickButton1)) { Debug.Log("JoystickButton1"); }
       
        }
    }
}