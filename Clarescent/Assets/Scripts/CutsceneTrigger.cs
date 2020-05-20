using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CutsceneTrigger : MonoBehaviour
{
    private bool triggered = false;

    [SerializeField] private Transform _cutsceneToTrigger;

    private void Start()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ClaraBehavior>() == null || triggered) return;

        CutsceneManager.ShowCutscene(_cutsceneToTrigger);
        triggered = true;
    }
}
