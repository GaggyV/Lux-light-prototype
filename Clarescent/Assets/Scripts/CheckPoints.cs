using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    private ReSpawner rs;

    void Start()
    {
        rs = GameObject.FindGameObjectWithTag("ReSpawner").GetComponent<ReSpawner>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Clara"))
        {
            rs.lastCheckpointPos = transform.position;
        }
    }
}
