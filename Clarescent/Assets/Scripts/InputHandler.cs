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

    [SerializeField] private bool dontRead = false;

    [SerializeField] Controller currentController;
    public AnalogTwoDimensionalInput leftStick;
    public AnalogTwoDimensionalInput rightStick;
    public AnalogOneDimensionalInput leftTriggerAnalog;
    public AnalogOneDimensionalInput rightTriggerAnalog;
    public DigitalInput grab;
    public DigitalInput leftTriggerDigital;
    public DigitalInput rightTriggerDigital;
    public DigitalInput PauseButton;

    private List<Joycon> joycons;

    [SerializeField] Settings settings;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        if (dontRead) return;
        currentController = settings.controller;
        joycons = JoyconManager.Instance.j;


        if (joycons.Count > 0)
        {
            Joycon j = joycons[0];
            j.Recenter();
        }
    }


    void Update()
    {
        switch (currentController)
        {
            case Controller.Keyboard:
                leftStick.x_axis = Input.GetKey(KeyCode.D) ? 1f : 0f;
                leftStick.x_axis = Input.GetKey(KeyCode.A) ? leftStick.x_axis - 1f : leftStick.x_axis;
                leftStick.y_axis = Input.GetKey(KeyCode.W) ? 1f : 0f;
                leftStick.y_axis = Input.GetKey(KeyCode.S) ? leftStick.y_axis - 1f : leftStick.y_axis;
    
                rightStick.x_axis = Input.GetAxis("Mouse X");
                rightStick.y_axis = Input.GetAxis("Mouse Y");


                leftTriggerAnalog.axis = Input.GetKey(KeyCode.Space) ? 1f : -1f;

                rightTriggerAnalog.axis = Input.GetKey(KeyCode.Mouse0  /*RightShift*/) ? 1f : -1f;
                //Cursor.visible = false; /*remove later on*/

                leftTriggerDigital.enter = Input.GetKeyDown(KeyCode.LeftControl);
                leftTriggerDigital.held = Input.GetKey(KeyCode.LeftControl);
                leftTriggerDigital.exit = Input.GetKeyUp(KeyCode.LeftControl);

                rightTriggerDigital.enter = Input.GetKeyDown(KeyCode.RightControl);
                rightTriggerDigital.held = Input.GetKey(KeyCode.RightControl);
                rightTriggerDigital.exit = Input.GetKeyUp(KeyCode.RightControl);

                grab.enter = Input.GetKeyDown(KeyCode.LeftShift);
                grab.held = Input.GetKey(KeyCode.LeftShift);
                grab.exit = Input.GetKeyUp(KeyCode.LeftShift);

                PauseButton.enter = Input.GetKeyDown(KeyCode.Escape);
                PauseButton.held = Input.GetKey(KeyCode.Escape);
                PauseButton.exit = Input.GetKeyUp(KeyCode.Escape);

                break;
            case Controller.Controller:


                leftStick.x_axis = Input.GetAxis("Horizontal");
                leftTriggerAnalog.axis = Input.GetAxis("XJump");

                rightStick.x_axis = Input.GetAxis("TVertical");
                rightStick.y_axis = Input.GetAxis("THorizontal"); 




                if (Input.GetAxis("Jump") > 1) { Debug.Log("JoystickButton0"); }
                if (Input.GetKeyDown(KeyCode.JoystickButton1)) { Debug.Log("JoystickButton1"); }
                if (Input.GetKeyDown(KeyCode.JoystickButton2)) { Debug.Log("JoystickButton2"); }
                //rightStick.x_axis = Input.GetAxis("RightStickHorizontal");
                //rightStick.y_axis = Input.GetAxis("RightStickVertical");


                break;
            case Controller.Unique:


                if (joycons.Count > 0)
                {
                    Joycon j = joycons[0];


                    Vector3 orientation = j.GetVector().eulerAngles;
                    rightStick.x_axis = -(orientation.y < 0 ? orientation.y + 180 : orientation.y - 180) / 180;
                    rightStick.y_axis = (orientation.x > 180 ? orientation.x - 360 : orientation.x) / 180;

                    rightTriggerDigital.enter = j.GetButtonDown(Joycon.Button.SHOULDER_1);
                    rightTriggerDigital.held = j.GetButton(Joycon.Button.SHOULDER_1);
                    rightTriggerDigital.exit = j.GetButtonUp(Joycon.Button.SHOULDER_1);

                    rightTriggerAnalog.axis = j.GetButton(Joycon.Button.SHOULDER_2) ? 1f : -1f;

                    j = joycons[1];

                    leftTriggerAnalog.axis = j.GetButton(Joycon.Button.SHOULDER_2) ? 1f : -1f;

                    leftStick.x_axis = j.GetStick()[0];
                    leftStick.y_axis = j.GetStick()[1];

                    grab.enter = j.GetButtonDown(Joycon.Button.SHOULDER_1);
                    grab.held = j.GetButton(Joycon.Button.SHOULDER_1);
                    grab.exit = j.GetButtonUp(Joycon.Button.SHOULDER_1);

                }
                break;
            default:
                break;
        }
        
    }
}