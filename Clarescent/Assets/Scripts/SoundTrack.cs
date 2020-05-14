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
    
    void Start()
    {
        source = GetComponent<AudioSource>();
        timing = false;
        timer = 0.0f;
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

    public void ChangeAudioClip(AudioClip clip)
    {
        source.clip = clip;
    }

}
