using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _tingToWakeUp;
    [SerializeField] SoundHandler soundHandler;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _tingToWakeUp.SetActive(true);
        soundHandler.TingWakeSFX();
        Destroy(gameObject);
    }
}
