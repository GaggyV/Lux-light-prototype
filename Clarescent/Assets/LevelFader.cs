using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFader : MonoBehaviour
{

    public Animator transition;

    public float transitionTime = 1f;
   
    void Update()
    {
        
    }
    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

    
    }
}
