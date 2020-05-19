using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DigitalInput
{
    public bool enter;
    public bool held;
    public bool exit;
}

public struct AnalogOneDimensionalInput
{
    public float axis;
}

public struct AnalogTwoDimensionalInput
{
    public float x_axis;
    public float y_axis;
}

public enum Controller { Keyboard, Controller, Unique }
public class InputHandler : MonoBehaviour
{
    [SerializeField] Controller currentController;
    public AnalogTwoDimensionalInput leftStick;
    public AnalogTwoDimensionalInput rightStick;
    public AnalogOneDimensionalInput leftTriggerAnalog;
    public AnalogOneDimensionalInput rightTriggerAnalog;
    public DigitalInput grab;
    public DigitalInput leftTriggerDigital;
    public DigitalInput rightTriggerDigital;
    public DigitalInput Start;
        
    void Update()
    {
        switch (currentController)
        {
            case Controller.Keyboard:
                leftStick.x_axis = Input.GetKey(KeyCode.D) ? 1f : 0f;
                leftStick.x_axis = Input.GetKey(KeyCode.A) ? leftStick.x_axis - 1f : leftStick.x_axis;
                leftStick.y_axis = Input.GetKey(KeyCode.W) ? 1f : 0f;
                leftStick.y_axis = Input.GetKey(KeyCode.S) ? leftStick.y_axis - 1f : leftStick.y_axis;
    
                rightStick.x_axis = Input.GetKey(KeyCode.RightArrow) ? 1f : 0f;
                rightStick.x_axis = Input.GetKey(KeyCode.LeftArrow) ? rightStick.x_axis - 1f : rightStick.x_axis;
                rightStick.y_axis = Input.GetKey(KeyCode.UpArrow) ? 1f : 0f;
                rightStick.y_axis = Input.GetKey(KeyCode.DownArrow) ? rightStick.y_axis - 1f : rightStick.y_axis;

                leftTriggerAnalog.axis = Input.GetKey(KeyCode.Space) ? 1f : -1f;

                rightTriggerAnalog.axis = Input.GetKey(KeyCode.Mouse0  /*RightShift*/) ? 1f : -1f;
                Cursor.visible = false; /*remove later on*/

                leftTriggerDigital.enter = Input.GetKeyDown(KeyCode.LeftControl);
                leftTriggerDigital.held = Input.GetKey(KeyCode.LeftControl);
                leftTriggerDigital.exit = Input.GetKeyUp(KeyCode.LeftControl);

                rightTriggerDigital.enter = Input.GetKeyDown(KeyCode.RightControl);
                rightTriggerDigital.held = Input.GetKey(KeyCode.RightControl);
                rightTriggerDigital.exit = Input.GetKeyUp(KeyCode.RightControl);

                grab.enter = Input.GetKeyDown(KeyCode.LeftShift);
                grab.held = Input.GetKey(KeyCode.LeftShift);
                grab.exit = Input.GetKeyUp(KeyCode.LeftShift);

                break;
            case Controller.Controller:

                leftStick.x_axis = Input.GetAxis("Horizontal");
                leftStick.y_axis = Input.GetAxis("Vertical");
                //leftTriggerAnalog.axis = Input.GetButtonDown(joystick b;
                //leftStick.x_axis = Input.GetJoystickNames

                break;
            default:
                break;
        }
        
    }
}