using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Symbol : MonoBehaviour
{
    [SerializeField] private Interactable[] interactables;
    [SerializeField] private GameObject[] drums;
    [SerializeField] private int[] sequenceRequired;
    [SerializeField] private Animation anim;
    [SerializeField] private AnimationClip ac;

    private int[] sequenceCurrent;

    void Start()
    {
        interactables[0].AddListener(delegate { OnInteract(0); });
        interactables[1].AddListener(delegate { OnInteract(1); });
        interactables[2].AddListener(delegate { OnInteract(2); });
        interactables[3].AddListener(delegate { OnInteract(3); });

        sequenceCurrent = new int[4];

        anim.AddClip(ac, ac.name);
    }

    private void OnInteract(int i)
    {
        sequenceCurrent[i]++;
        if (sequenceCurrent[i] == 8)
        {
            sequenceCurrent[i] = 0;
        }
        drums[i].transform.localEulerAngles = new Vector3(-sequenceCurrent[i] * 45, 0, 0);

        if (IsSequenceCorrect())
        {
            foreach (Interactable interactable in interactables)
            {
                interactable.gameObject.SetActive(false);
            }
            anim.Play(ac.name);
            Debug.Log("done!");
        }
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
