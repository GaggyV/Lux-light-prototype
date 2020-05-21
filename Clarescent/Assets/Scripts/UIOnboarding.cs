using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOnboarding : MonoBehaviour
{
    public GameObject uiObject;

    void Start()
    {
        uiObject.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Clara")
        {
            uiObject.SetActive(true);
            StartCoroutine("WaitForSec");
        }
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(5);
        Destroy(uiObject);
        Destroy(gameObject);
    }

}
    
