using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnSFX : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audioSrc;
    public AudioClip Btnsound;

     public void onClick()
    {
        audioSrc.PlayOneShot(Btnsound);
    }

}
