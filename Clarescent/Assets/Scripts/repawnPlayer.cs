using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class repawnPlayer : MonoBehaviour
{
    [SerializeField] Transform Respawn;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Clara"))
        {
            collision.transform.position = Respawn.position;
        }
    }
}
