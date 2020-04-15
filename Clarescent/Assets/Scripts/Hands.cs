using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands : MonoBehaviour
{
    [SerializeField] InputHandler inputHandler;
    ClaraInteractable interactor;
    [SerializeField] float intensity;

    void Update()
    {
        if (inputHandler.leftTriggerDigital.held && interactor != null)
        {
            interactor.body.velocity = new Vector2((transform.position - interactor.transform.position).x * intensity, interactor.body.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        interactor = other.GetComponent<ClaraInteractable>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        interactor = null;
    }

}
