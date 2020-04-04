using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TingBehavior : MonoBehaviour
{
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Color levitationColor, negentropyColor, illuminationColor, shineDifference;
    private Color currentColor;
    public enum Ability { levitation, negentropy, illumination, goBack }
    [SerializeField] Ability currentAbility;

    void Start()
    {
        currentColor = levitationColor;
    }

    void Update()
    {
        if (inputHandler.rightStick.x_axis != 0f || inputHandler.rightStick.y_axis != 0f)
            transform.position += new Vector3(inputHandler.rightStick.x_axis, inputHandler.rightStick.y_axis, 0f) * moveSpeed * Time.deltaTime;
        if (inputHandler.rightTriggerDigital.enter)
        {
            currentAbility++;
            if (currentAbility == Ability.goBack)
                currentAbility = 0;
            switch (currentAbility)
            {
                case Ability.illumination:
                    currentColor = illuminationColor;
                    break;
                case Ability.levitation:
                    currentColor = levitationColor;
                    break;                     
                case Ability.negentropy:       
                    currentColor = negentropyColor;
                    break;
            }
        }
        if (inputHandler.rightTriggerAnalog.axis != -1f)
            GetComponent<SpriteRenderer>().color = currentColor;
        else
            GetComponent<SpriteRenderer>().color = currentColor - shineDifference;
    }


}