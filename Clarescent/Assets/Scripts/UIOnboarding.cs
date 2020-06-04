using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOnboarding : MonoBehaviour
{
    public GameObject uiObject;
    public UIOnboarding lastUIObject;

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
            if (lastUIObject != null)
            {
                Destroy(lastUIObject.uiObject);
                Destroy(lastUIObject.gameObject);
            }
        }
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(4);
        Destroy(uiObject);
        Destroy(gameObject);
    }

}
    
