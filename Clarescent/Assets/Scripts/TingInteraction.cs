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
    private BoxCollider2D collider2D;

    [SerializeField] Sprite restoredSprite, brokenSprite;
    [SerializeField] SpriteRenderer outLine;
    CapsuleCollider2D bigBox;
    BoxCollider2D smallBox;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        bigBox = GetComponent<CapsuleCollider2D>();
        smallBox = GetComponent<BoxCollider2D>();
        smallBox.enabled = false;
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        currentVelocity = body.velocity.magnitude;
        if (broken && spriteRenderer.sprite != brokenSprite)
        {
            spriteRenderer.sprite = brokenSprite;
            if (outLine != null)
                outLine.enabled = false;
            if (bigBox != null)
                bigBox.enabled = false;
            if (smallBox != null)
                smallBox.enabled = true;
        }
        if (!broken && spriteRenderer.sprite != restoredSprite)
        {
            spriteRenderer.sprite = restoredSprite;
            if (outLine != null)
                outLine.enabled = true;
            if (bigBox != null)
                bigBox.enabled = true;
            if (smallBox != null)
                smallBox.enabled = false;
        }
    }


    public bool IsFreeToMove(ClaraBehavior clara)
    {
        if (clara == null)
            return true;

        if(clara != null)
        {
            return Mathf.Abs(clara.transform.position.x - transform.position.x) < collider2D.size.x / 2 && clara.transform.position.y < transform.position.y;
        }

        return false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (currentVelocity >= breakSpeed)
        {
            broken = true;
            //GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }   

    private void OnCollisionExit2D(Collision2D other)
    {

    }

    private void OnDrawGizmos()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Floor(pos.x);
        pos.y = Mathf.Floor(pos.y);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(pos, pos + Vector3.right);
        Gizmos.DrawLine(pos + Vector3.up, pos + Vector3.right + Vector3.up);
        Gizmos.DrawLine(pos, pos + Vector3.up);
        Gizmos.DrawLine(pos + Vector3.right, pos + Vector3.right + Vector3.up);

        Gizmos.color = Color.green;


        
    }

}
