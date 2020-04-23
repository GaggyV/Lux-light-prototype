using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mPosition : MonoBehaviour
{
    private Vector3 mPositiion;
    private Rigidbody2D rb;
    private Vector2 direction;
    public float movespeed = 100f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        mPositiion = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mPositiion - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * movespeed, direction.y * movespeed);
    }
}
