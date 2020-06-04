using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private Transform _level;
    [SerializeField] private List<Transform> _slides;

    [SerializeField] private float _slideTime;

    private InputHandler _input;

    private static CutsceneManager manager;

    private void Awake()
    {
        _input = FindObjectOfType<InputHandler>();

        manager = this;
        //foreach (var t in transform.GetComponentInChildren<Canvas>().transform.GetComponentsInChildren<Transform>())
        //{
        //    if (t.GetComponent<Canvas>() != null) continue;
        //    t.gameObject.SetActive(false);
        //}

        var canvas = transform.GetComponentInChildren<Canvas>();
        for (int i = 0; i < canvas.transform.childCount; i++)
        {
            canvas.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        manager = null;
    }

    public static void ShowCutscene(Transform cutscene)
    {
        if(manager == null)
        {
            Debug.LogError("You're trying to play a cutscene but there is no cutscenemanager!");
            return;
        }
        manager.gameObject.SetActive(true);
        manager.StartCoroutine(manager.PlayCutscene(cutscene));
    }

    private IEnumerator PlayCutscene(Transform cutscene)
    {
        Transform cam = Camera.main.transform;
        Transform cameraParent = cam.parent;

        Vector3 camPos = cam.position;

        cam.transform.parent = transform;
        cam.gameObject.SetActive(true);

        var inputParent = _input.transform.parent;
        _input.transform.parent = transform;

        _level.gameObject.SetActive(false);
        cutscene.gameObject.SetActive(true);
        print($"Cutscene triggered with {cutscene.childCount } slides");
        for(int i = 0; i < cutscene.childCount; i++)
        {
            if (cutscene.GetChild(i).CompareTag("PersistentSlide"))
            {
                cutscene.GetChild(i).gameObject.SetActive(true);
            }
            else cutscene.GetChild(i).gameObject.SetActive(false);
        }
        for (int i = 0; i < cutscene.childCount; i++)
        {
            if (cutscene.GetChild(i).CompareTag("PersistentSlide"))
            {
                continue;
            }


            cutscene.GetChild(i).gameObject.SetActive(true);
            //yield return new WaitForSeconds(_slideTime);

            bool next = false;
            float startTime = Time.time;

            /*
             If some smarter solution is found please put it here
            */

            while(!next)
            {
                if (Time.time > startTime + _slideTime || _input.leftTriggerDigital.enter)
                    next = true;
                yield return null;
            }
                 

            cutscene.GetChild(i).gameObject.SetActive(false);
        }
        cutscene.gameObject.SetActive(false);
        _level.gameObject.SetActive(true);
        _input.transform.parent = inputParent; //this may or may not be important
        cam.parent = cameraParent;
        cam.position = camPos;
        gameObject.SetActive(false);
        yield return null;
    }
}
