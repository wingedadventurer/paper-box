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
    public AudioClip sfxKeyTurn;
    public AudioClip sfxSwitch;
    public AudioClip sfxInsert;
    public AudioClip sfxPlace;
    public AudioClip sfxPlatePush;
    public AudioClip sfxSlide;

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

    public AudioSource PlaySFX(AudioClip clip)
    {
        GameObject sfx = Instantiate(prefabSFX, transform.position, Quaternion.identity);
        AudioSource ass = sfx.GetComponent<AudioSource>();
        ass.clip = clip;
        ass.Play();
        return ass;
    }

    public AudioSource PlaySFX(AudioClip clip, Vector3 globalPosition)
    {
        AudioSource ass = PlaySFX(clip);
        ass.spatialBlend = 1.0f;
        ass.transform.position = globalPosition;
        return ass;
    }
}