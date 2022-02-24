using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Symbol : MonoBehaviour
{
    private const int DRUM_COUNT = 4;

    [SerializeField] private Interactable[] interactables;
    [SerializeField] private GameObject[] drums;
    [SerializeField] private int[] sequenceRequired;
    [SerializeField] private Animation anim;
    [SerializeField] private AnimationClip ac;

    private int[] sequenceCurrent = new int[DRUM_COUNT];
    private float[] drumAngles = new float[DRUM_COUNT];

    void Start()
    {
        for (int i = 0; i < DRUM_COUNT; i++)
        {
            int a = i;
            interactables[i].AddListener(delegate { OnInteract(a); });
        }

        anim.AddClip(ac, ac.name);
    }

    private void Update()
    {
        // update drum rotations
        for (int i = 0; i < DRUM_COUNT; i++)
        {
            drumAngles[i] = Mathf.LerpAngle(drumAngles[i], -sequenceCurrent[i] * 45, 0.1f);
            drums[i].transform.localEulerAngles = new Vector3(drumAngles[i], 0, 0);
        }
    }

    private void OnInteract(int i)
    {
        sequenceCurrent[i]++;
        if (sequenceCurrent[i] == 8)
        {
            sequenceCurrent[i] = 0;
        }

        if (IsSequenceCorrect())
        {
            foreach (Interactable interactable in interactables)
            {
                interactable.gameObject.SetActive(false);
            }
            anim.Play(ac.name);
        }

        AudioManager.instance.PlaySFX(AudioManager.instance.sfxTurn).SetVolume(0.6f);
    }

    private bool IsSequenceCorrect()
    {
        for (int i = 0; i < 4; i++)
        {
            if (sequenceCurrent[i] != sequenceRequired[i])
            {
                return false;
            }
        }
        return true;
    }
}
