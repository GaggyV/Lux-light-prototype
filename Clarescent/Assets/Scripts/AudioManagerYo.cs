using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerYo : MonoBehaviour
{
    [SerializeField] static AudioSource JupSFX;
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
//            AudioManagerYo.PlaySound("ClaraWalk");

/* public class Feet : MonoBehaviour
{
public bool onGround;
public static AudioSource jumpSound;

private void Start()
{
    jumpSound = GetComponent<AudioSource>();
}

private void OnCollisionEnter2D(Collision2D collision)
{
    jumpSound.Play();
    if (!collision.gameObject.CompareTag("Clara")) onGround = true;
}
private void OnCollisionExit2D(Collision2D collision)
{
    onGround = true;
}

}*/
