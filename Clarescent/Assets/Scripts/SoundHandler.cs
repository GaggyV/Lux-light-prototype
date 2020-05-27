using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    [SerializeField] Settings settings;
    [SerializeField] SoundTrack musicTrack;
    [SerializeField] SoundTrack walkingTrack;
    [SerializeField] SoundTrack SFXTrack0;
    [SerializeField] SoundTrack SFXTrack1;
    [SerializeField] SoundTrack LeviTrack;
    [SerializeField] SoundTrack LeviLoop;
    [SerializeField] SoundTrack ClaraWalk;
    [SerializeField] SoundTrack ClaraLanding;
    

    public bool walking;
    private bool walkingLagger;
    public AudioClip jumpingClip;
    //AudioSource source;
    void Start()
    {
        musicTrack.ChangeVolume(settings.musicVolume);
        walkingTrack.ChangeVolume(settings.soundEffectsVolume);
        SFXTrack0.ChangeVolume(settings.soundEffectsVolume);
        SFXTrack1.ChangeVolume(settings.soundEffectsVolume);
        LeviTrack.ChangeVolume(settings.soundEffectsVolume);
        LeviLoop.ChangeVolume(settings.soundEffectsVolume);
        ClaraWalk.ChangeVolume(settings.soundEffectsVolume);
        ClaraLanding.ChangeVolume(settings.soundEffectsVolume);
        //source = GetComponent<AudioSource>();
    }

    void Update()
    {
        //if (walking != walkingLagger && walking)
        //{
        //    walkingLagger = walking;
        //    source.clip = walkingClip;
        //    source.loop = true;
        //    source.Play();
        //}
        //else if (walking != walkingLagger && !walking)
        //{
        //    walkingLagger = walking;
        //    source.clip = null;
        //    source.loop = false;
    }

    public void Jump()
    {
        if (SFXTrack0.free)
        {
            SFXTrack0.Play(jumpingClip);
        }
        else if (SFXTrack1.free)
        {
            SFXTrack1.Play(jumpingClip);
        }
    }

    public void LevitateSFX()
    {
        if(LeviTrack.free)
        {
            LeviTrack.Play();
        }

        if (LeviLoop.free)
        {
            LeviLoop.Loop();
            LeviLoop.Play();
        }
    }

    public void LevitateSFXStop()
    {
        LeviLoop.StopLoop();
        LeviLoop.FadeOut();
    }

    public void ClaraWalkSFX()
    {
        if (ClaraWalk.free)
        {
            //source.PlayOneShot(walkingClip, 0.5f);
            ClaraWalk.Play();
            Invoke("Fadeout", (1.2f));
        }
    }

    public void Fadeout()
    {
        ClaraWalk.StopPlay();
    }


    public void CLandingSFX()
    {
        ClaraLanding.Play();
    }

}
