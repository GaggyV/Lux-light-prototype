using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TingBehavior : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private Sprite levitationSprite, negentropySprite, illuminationSprite, notShiningSprite;
    private Sprite currentIfShine;
    public enum Ability { levitation, negentropy, illumination, goBack }
    [SerializeField] Ability currentAbility;
    private bool shine;

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
                    currentIfShine = illuminationSprite;
                    break;
                case Ability.levitation:
                    currentIfShine = levitationSprite;
                    break;
                case Ability.negentropy:
                    currentIfShine = negentropySprite;
                    break;
            }
        }
    }
}