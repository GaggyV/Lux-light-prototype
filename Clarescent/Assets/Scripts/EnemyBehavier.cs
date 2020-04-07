using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavier : MonoBehaviour
{
    [SerializeField] float MoveSpeed = 1f;
    Rigidbody2D RidigBotty;
    bool FaceRight;

    // Start is called before the first frame update
    void Start()
    {
        RidigBotty = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(IsFaceRight())
        {
            RidigBotty.velocity = new Vector2(MoveSpeed, RidigBotty.velocity.y);
        }
        else
        {
            RidigBotty.velocity = new Vector2(-MoveSpeed, RidigBotty.velocity.y);
        }

    }

    bool IsFaceRight()
    {
        return transform.localScale.x > 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        transform.localScale *= new Vector2(-1f, 1f);
    }
}
