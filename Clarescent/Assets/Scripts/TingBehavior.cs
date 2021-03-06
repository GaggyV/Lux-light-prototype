﻿using System.Collections;
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
    [SerializeField] private ClaraBehavior clara;
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private List<TingInteraction> interactors;
    [SerializeField] private TextMesh text;
    [SerializeField] private SoundHandler soundHandler;
    private bool levitating;
    private bool levitateLagger;
    SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        levitating = levitateLagger = false;
        currentColor = levitationColor;
        //notShiningSprite = 
    }

    void Update()
    {
        if ((inputHandler.rightStick.x_axis != 0f || inputHandler.rightStick.y_axis != 0f))
            transform.position += new Vector3(inputHandler.rightStick.x_axis, inputHandler.rightStick.y_axis, 0f) * moveSpeed * Time.deltaTime;
        if (!sr.isVisible)
            transform.position -= new Vector3(inputHandler.rightStick.x_axis, inputHandler.rightStick.y_axis, 0f) * moveSpeed * Time.deltaTime;
        if (levitating && !levitateLagger)
        {
            soundHandler.LevitateSFX();

        }
        else if (!levitating && levitateLagger)
        {
            soundHandler.LevitateSFXStop();

        }
        levitateLagger = levitating;
        levitating = false;
        if (inputHandler.rightTriggerDigital.enter)
        {
            currentAbility++;
            if (currentAbility == Ability.goBack)
                currentAbility = 0;
            switch (currentAbility)
            {
                case Ability.illumination:
                    currentColor = illuminationColor;
                    text.text = "Illumination";
                    break;
                case Ability.levitation:
                    currentColor = levitationColor;
                    text.text = "Levitation";
                    break;                     
                case Ability.negentropy:       
                    currentColor = negentropyColor;
                    text.text = "Negentropy";
                    break;
            }
        }
        if (inputHandler.rightTriggerAnalog.axis != -1f)
            GetComponent<SpriteRenderer>().color = currentColor;
        else
            GetComponent<SpriteRenderer>().color = currentColor - shineDifference;
        foreach (var interactor in interactors)
        {
            switch (currentAbility)
            {
                case Ability.levitation:
                    if (interactor.canLevitate)
                    {
                        if(inputHandler.rightTriggerAnalog.axis > 0f)
                        {
                            levitating = true;
                            if (interactor.IsFreeToMove(clara))
                            {
                                if (Mathf.Abs(transform.position.y - interactor.transform.position.y) < 0.4f)
                                    interactor.body.velocity = new Vector2(interactor.body.velocity.x, 0f); /* * (inputHandler.rightTriggerAnalog.axis > 0f ? inputHandler.rightTriggerAnalog.axis : 0f)*/
                                else
                                    interactor.body.velocity = new Vector2(interactor.body.velocity.x, Mathf.Sign(transform.position.y - interactor.transform.position.y) * levitationStrength);
                            }
                        }

                        //interactor.body.velocity += Vector2.up * levitationStrength * Time.deltaTime * (inputHandler.rightTriggerAnalog.axis > 0f ? inputHandler.rightTriggerAnalog.axis : 0f);
                    }
                    break;
                case Ability.negentropy:
                    if (inputHandler.rightTriggerAnalog.axis > 0f)
                    {
                        interactor.broken = false;
                        //interactor.GetComponent<BoxCollider2D>().isTrigger = false;
                    }
                    break;
                case Ability.illumination:
                    if (inputHandler.rightTriggerAnalog.axis > 0f && interactor.canBeScared)
                        interactor.body.velocity += new Vector2(interactor.transform.position.x - transform.position.x, 0).normalized * interactor.scareSpeed * inputHandler.rightTriggerAnalog.axis * Time.deltaTime;
                    break;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<TingInteraction>() == null)
            return;

        interactors.Add(other.GetComponent<TingInteraction>());
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<TingInteraction>() == null)
            return;

        interactors.Remove(other.GetComponent<TingInteraction>());
    }
    
}