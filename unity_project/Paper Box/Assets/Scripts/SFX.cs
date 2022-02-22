using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    AudioSource ass;

    private void Awake()
    {
        ass = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!ass.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
