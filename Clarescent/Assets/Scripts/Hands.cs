using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands : MonoBehaviour
{
    [SerializeField] InputHandler inputHandler;
    public ClaraInteractable interactor;
    [SerializeField] float intensity;

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        interactor = other.GetComponent<ClaraInteractable>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(interactor != null)
            interactor.body.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
        interactor = null;
    }

}
