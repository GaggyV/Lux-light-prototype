using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    private ReSpawner rs;
    public Sprite CPchecked;
    public GameObject ting;

    void Start()
    {
        rs = GameObject.FindGameObjectWithTag("ReSpawner").GetComponent<ReSpawner>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Clara"))
        {
            rs.lastCheckpointPos = transform.position;
            if (ting.activeSelf) rs.hasTing = true;
            gameObject.GetComponent<SpriteRenderer>().sprite = CPchecked;
        }
    }
}
