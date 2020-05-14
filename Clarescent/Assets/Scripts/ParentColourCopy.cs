using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentColourCopy : MonoBehaviour
{
    private SpriteRenderer spriteRenderer, parentSpriteRenderer;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        parentSpriteRenderer = transform.parent.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (spriteRenderer == null || parentSpriteRenderer == null) return;
        spriteRenderer.color = parentSpriteRenderer.color;
    }
}
