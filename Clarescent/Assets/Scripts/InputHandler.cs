using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHandDigitalInput
{
    public bool upEnter = false;
    public bool leftEnter = false;
    public bool downEnter = false;
    public bool rightEnter = false;
    public bool upPressed = false;
    public bool leftPressed = false;
    public bool downPressed = false;
    public bool rightPressed = false;
    public bool upExit = false;
    public bool leftExit = false;
    public bool downExit = false;
    public bool rightExit = false;
}

public class LeftHandAnalogInput
{
    public float x = 0f;
    public float y = 0f;
}

public class RightHandAnalogInput
{
    public float x = 0f;
    public float y = 0f;
}

public class JumpButtonDigitalInput
{
    public bool enter = false;
    public bool pressed = false;
    public bool exit = false;
}

public class InputHandler : MonoBehaviour
{
    public LeftHandDigitalInput leftHandDigital;
    public LeftHandAnalogInput leftHandAnalog;
    public RightHandAnalogInput rightHandAnalog;
    public JumpButtonDigitalInput jumpDigital;
        
    void Update()
    {
        leftHandDigital.upEnter = Input.GetKeyDown(KeyCode.Keypad8);
        leftHandDigital.upPressed = Input.GetKey(KeyCode.Keypad8);
        leftHandDigital.upExit = Input.GetKeyUp(KeyCode.Keypad8);
        leftHandDigital.leftEnter = Input.GetKeyDown(KeyCode.Keypad4);
        leftHandDigital.leftPressed = Input.GetKey(KeyCode.Keypad4);
        leftHandDigital.leftExit = Input.GetKeyUp(KeyCode.Keypad4);
        leftHandDigital.downEnter = Input.GetKeyDown(KeyCode.Keypad2);
        leftHandDigital.downPressed = Input.GetKey(KeyCode.Keypad2);
        leftHandDigital.downExit = Input.GetKeyUp(KeyCode.Keypad2);
        leftHandDigital.rightEnter = Input.GetKeyDown(KeyCode.Keypad6);
        leftHandDigital.rightPressed = Input.GetKey(KeyCode.Keypad6);
        leftHandDigital.rightExit = Input.GetKeyUp(KeyCode.Keypad6);

        leftHandAnalog.y = leftHandAnalog.y += Input.GetKey(KeyCode.W) && leftHandAnalog.y < 1f ? 1f : 0f;
        leftHandAnalog.y = leftHandAnalog.y += Input.GetKey(KeyCode.S) && leftHandAnalog.y > -1f ? -1f : 0f;
        leftHandAnalog.x = leftHandAnalog.x += Input.GetKey(KeyCode.A) && leftHandAnalog.y > -1f ? -1f : 0f;
        leftHandAnalog.x = leftHandAnalog.y += Input.GetKey(KeyCode.D) && leftHandAnalog.y < 1f ? 1f : 0f;
        rightHandAnalog.y = rightHandAnalog.y += Input.GetKey(KeyCode.UpArrow) && rightHandAnalog.y < 1f ? 1f : 0f;
        rightHandAnalog.y = rightHandAnalog.y += Input.GetKey(KeyCode.DownArrow) && rightHandAnalog.y > -1f ? -1f : 0f;
        rightHandAnalog.x = rightHandAnalog.x += Input.GetKey(KeyCode.LeftArrow) && rightHandAnalog.y > -1f ? -1f : 0f;
        rightHandAnalog.x = rightHandAnalog.y += Input.GetKey(KeyCode.RightArrow) && rightHandAnalog.y < 1f ? 1f : 0f;

        jumpDigital.enter = Input.GetKeyDown(KeyCode.Space);
        jumpDigital.pressed = Input.GetKey(KeyCode.Space);
        jumpDigital.exit = Input.GetKeyUp(KeyCode.Space);
    }
}