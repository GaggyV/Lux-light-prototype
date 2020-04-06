using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TingBehavior : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float levitationStrength;
    [SerializeField] private Color levitationColor, negentropyColor, illuminationColor, shineDifference;
    private Color currentColor;
    public enum Ability { levitation, negentropy, illumination, goBack }
    [SerializeField] Ability currentAbility;

    [Header("Don't touch me Victor D:")]
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private List<TingInteraction> interactors;
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
        if (Input.GetKeyDown(KeyCode.Space)) print(interactors);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<TingInteraction>() == null) return;
        interactors.Add(other.GetComponent<TingInteraction>());
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<TingInteraction>() == null) return;
        interactors.Remove(other.GetComponent<TingInteraction>());
    }
    /*void OnTriggerStay2D(Collider2D other)
    {
        //if (inputHandler.rightTriggerAnalog == -1f) return; This line caused issues, so we're doing it differently
        TingInteraction interactor = other.GetComponent<TingInteraction>();
        if (interactor == null) return;
        switch (currentAbility)
        {
            case Ability.levitation:
                if (interactor.canLevitate)
                    interactor.body.velocity += Vector2.up * levitationStrength * Time.deltaTime * (inputHandler.rightTriggerAnalog.axis > 0f ? inputHandler.rightTriggerAnalog.axis : 0f);
                break;
            case Ability.negentropy:
                if (inputHandler.rightTriggerAnalog.axis > 0f)
                    interactor.broken = false; 
                break;
            case Ability.illumination:
                if (inputHandler.rightTriggerAnalog.axis > 0f && interactor.canBeScared)
                    interactor.body.velocity += new Vector2(other.transform.position.x - transform.position.x, 0).normalized * interactor.scareSpeed * inputHandler.rightTriggerAnalog.axis * Time.deltaTime;
                break;
        }
        return;
    }*/
}