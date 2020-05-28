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
    [SerializeField] SpriteRenderer outLine;

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
        {
            spriteRenderer.sprite = brokenSprite;
            if (outLine != null)
                outLine.enabled = false;
        }
        if (!broken && spriteRenderer.sprite != restoredSprite)
        {
            spriteRenderer.sprite = restoredSprite;
            if (outLine != null)
                outLine.enabled = true;
        }
    }

    private ClaraBehavior clara = null;

    public bool IsFreeToMove()
    {
        if (clara == null)
            return true;

        if(clara != null)
        {
            Vector2 delta = clara.transform.position - transform.position;
            if (delta.y < delta.x)
                return true;
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


        if (other.gameObject.GetComponent<ClaraBehavior>() != null)
            clara = other.gameObject.GetComponent<ClaraBehavior>();
    }   

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<ClaraBehavior>() != null)
            clara = null;
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

        if (clara == null)
            return;

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, clara.transform.position);

        
    }

}
