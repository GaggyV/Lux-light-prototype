using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrack : MonoBehaviour
{

    public bool free;
    AudioSource source;
    private float timer;
    private float terminateTime;
    private bool timing;
    private bool fadingOut;
    private float volume;
    
    void Start()
    {
        source = GetComponent<AudioSource>();
        timing = false;
        timer = 0.0f;
        volume = source.volume;
        fadingOut = false;
    }

    void Update()
    {
        free = !source.isPlaying;
        
        if (timing)
            timer += Time.deltaTime;
        if (timer > terminateTime)
        {
            timing = false;
            StopLoop();
        }

        if (fadingOut)
        {
            if (source.volume > 0.1f)
                source.volume *= 0.9f;
            else
            {
                source.Stop();
                source.volume = volume;
                fadingOut = false;
            }
        }

    }

    public void Play()
    {
        if (source.clip != null)
            source.Play();
    }

    public void Play(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }

    public void Loop()
    {
        source.loop = true;
    }
    public void Loop(float time)
    {
        timing = true;
        terminateTime = time;
    }

    public void StopLoop()
    {
        source.loop = false;
    }
    public void StopPlay()
    {
        source.Stop();
    }
    public void FadeOut()
    {
        fadingOut = true;
    }

    public void ChangeAudioClip(AudioClip clip)
    {
        source.clip = clip;
    }

}
