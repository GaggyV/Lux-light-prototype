using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    private ReSpawner rs;
    public Sprite CPchecked;

    void Start()
    {
        rs = GameObject.FindGameObjectWithTag("ReSpawner").GetComponent<ReSpawner>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Clara"))
        {
            rs.lastCheckpointPos = transform.position;
            gameObject.GetComponent<SpriteRenderer>().sprite = CPchecked;
        }
    }
}
