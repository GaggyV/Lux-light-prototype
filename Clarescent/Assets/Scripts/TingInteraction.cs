using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TingInteraction : MonoBehaviour
{
    public bool canLevitate;
    public bool canBreak;
    public float breakSpeed;
    public bool canBeScared;
    public float scareSpeed;
    public Rigidbody2D body;
    public bool broken;
    public bool scared;
    private float currentVelocity;

    [SerializeField] Sprite restoredSprite, brokenSprite;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        currentVelocity = body.velocity.magnitude;
        if (broken && spriteRenderer.sprite != brokenSprite)
            spriteRenderer.sprite = brokenSprite;
        if (!broken && spriteRenderer.sprite != restoredSprite)
            spriteRenderer.sprite = restoredSprite;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (currentVelocity >= breakSpeed)
        {
            broken = true;
        }
    }

}
