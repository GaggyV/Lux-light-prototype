using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _tingToWakeUp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _tingToWakeUp.SetActive(true);
        Destroy(gameObject);
    }
}
