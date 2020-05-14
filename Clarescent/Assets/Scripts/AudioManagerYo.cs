using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerYo : MonoBehaviour
{
    static AudioClip JumpSFX;
    static AudioClip ClaraWalkSFX;

    static AudioSource audioSrc;

    void Start()
    {
        JumpSFX = Resources.Load<AudioClip>("clara_jump");
        ClaraWalkSFX = Resources.Load<AudioClip>("walking_loop_1");

        audioSrc = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "jump":
                audioSrc.PlayOneShot(JumpSFX);
                break;
            case "ClaraWalk":
                audioSrc.PlayOneShot(ClaraWalkSFX);
                break;
        }
    }

}