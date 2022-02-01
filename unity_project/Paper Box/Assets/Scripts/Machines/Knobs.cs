using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knobs : MonoBehaviour
{
    public Interactable[] INsKnobs;
    public Knob[] knobs;
    public int[] sequence;
    public Animation anim;

    public void OnKnobTurn(int i)
    {
        if (IsCompleted())
        {
            foreach (Interactable interactable in INsKnobs)
            {
                interactable.SetActive(false);
            }

            anim.Play();
        }
    }

    private bool IsCompleted()
    {
        for(int i = 0; i < 6; i++)
        {
            if (knobs[i].dir != sequence[i])
            {
                return false;
            }
        }

        anim.Play();
        return true;
    }
}
