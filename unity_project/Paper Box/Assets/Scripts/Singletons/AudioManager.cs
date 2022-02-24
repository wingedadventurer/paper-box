using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public GameObject prefabSFX;

    [Header("Music")]
    public AudioClip musicMenu;
    public AudioClip musicGame;

    [Header("SFX")]
    public AudioClip sfxGameButton;
    public AudioClip sfxPickup;
    public AudioClip sfxTurn;
    public AudioClip sfxKeyInsert;
    public AudioClip sfxKeyTurn;
    public AudioClip sfxSwitch;
    public AudioClip sfxPlace;
    public AudioClip sfxPlatePush;
    public AudioClip sfxSlide;
    public AudioClip sfxWater;
    public AudioClip sfxIgnite;
    public AudioClip sfxInsert;
    public AudioClip sfxPut;
    public AudioClip sfxBreak;
    public AudioClip sfxButtonHover;
    public AudioClip sfxButtonClick;
    public AudioClip sfxSmolDoor;
    public AudioClip sfxMechanicalPull;
    public AudioClip sfxDoor;

    private AudioSource asMusic;

    private void Awake()
    {
        if (instance)
        {
            Destroy(this);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        GameObject go = new GameObject("Music");
        go.transform.SetParent(transform);
        asMusic = go.AddComponent<AudioSource>();
    }

    public void PlayMusic(AudioClip clip)
    {
        if (asMusic.clip == clip) { return; }

        asMusic.Stop();
        asMusic.clip = clip;
        asMusic.Play();
    }

    public SFX CreateSFX()
    {
        GameObject goSFX = Instantiate(prefabSFX, transform);
        return goSFX.GetComponent<SFX>();
    }

    public SFX PlaySFX(AudioClip clip)
    {
        SFX sfx = CreateSFX();
        sfx.SetClip(clip);
        sfx.Play();
        return sfx;
    }

    public SFX PlaySFX(AudioClip clip, float delay)
    {
        SFX sfx = CreateSFX().SetClip(clip).SetDelay(delay).Play();
        return sfx;
    }

    public AudioSource PlaySFX(AudioClip clip, Vector3 globalPosition)
    {
        GameObject sfx = Instantiate(prefabSFX, transform);
        AudioSource ass = sfx.GetComponent<AudioSource>();
        ass.transform.position = globalPosition;
        ass.spatialBlend = 1.0f;
        ass.clip = clip;
        ass.Play();
        return ass;
    }
}
