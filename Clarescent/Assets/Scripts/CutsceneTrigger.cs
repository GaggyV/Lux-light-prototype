using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CutsceneTrigger : MonoBehaviour
{
    private enum InvokeType { Collision, GameStart };

    [SerializeField] private InvokeType _invokeType;

    private bool triggered = false;

    [SerializeField] private Transform _cutsceneToTrigger;

    private void Start()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
        if(_invokeType == InvokeType.Collision)
        {
            TriggerCutScene();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ClaraBehavior>() == null || triggered || _invokeType != InvokeType.Collision) return;

        TriggerCutScene();
    }

    private void TriggerCutScene()
    {
        if (triggered) return;
        CutsceneManager.ShowCutscene(_cutsceneToTrigger);
        triggered = true;
    }
}
