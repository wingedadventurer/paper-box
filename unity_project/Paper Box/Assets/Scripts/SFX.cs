using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    AudioSource ass;

    private float delay;
    private bool playQueued;

    private void Awake()
    {
        ass = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (playQueued)
        {
            if (delay > 0)
            {
                delay -= Time.deltaTime;
            }
            if (delay <= 0)
            {
                ass.Play();
                playQueued = false;
            }
        }
        else
        {
            if (!ass.isPlaying)
            {
                Destroy(gameObject);
            }
        }
    }

    public SFX SetClip(AudioClip clip)
    {
        ass.clip = clip;
        return this;
    }

    public SFX SetVolume(float value)
    {
        ass.volume = value;
        return this;
    }

    public SFX SetDelay(float value)
    {
        delay = value;
        return this;
    }

    public SFX SetPosition(Vector3 position)
    {
        transform.position = position;
        ass.spatialBlend = 1.0f;
        return this;
    }

    public SFX Play()
    {
        playQueued = true;
        return this;
    }

    private void _Play()
    {
        ass.Play();
    }
}
