using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TingBehavior : MonoBehaviour
{
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float levitationStrength;
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

    void OnTriggerStay2D(Collider2D other)
    {

        print(inputHandler.rightTriggerAnalog.axis);
        if (inputHandler.rightTriggerAnalog.axis == -1f) return;

        print("1");
        TingInteraction interactor = other.GetComponent<TingInteraction>();
        if (interactor == null) return;
        switch (currentAbility)
        {
            case Ability.levitation:
                if (interactor.canLevitate)
                    interactor.body.velocity += Vector2.up * levitationStrength * Time.deltaTime * inputHandler.rightTriggerAnalog.axis;
                return;
            case Ability.negentropy:
                print("2");
                interactor.broken = false;
                return;
            case Ability.illumination:
                if (interactor.canBeScared)
                    other.transform.position += (other.transform.position - transform.position).normalized;
                return;
        }


    }
}