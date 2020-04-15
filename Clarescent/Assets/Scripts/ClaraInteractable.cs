using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClaraInteractable : MonoBehaviour
{
    public Rigidbody2D body;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

}
