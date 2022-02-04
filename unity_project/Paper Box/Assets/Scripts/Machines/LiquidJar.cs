using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidJar : MonoBehaviour
{
    [SerializeField] private Animation anim;
    [SerializeField] private bool[] sequence;
    [SerializeField] private Interactable[] inBowls;
    [SerializeField] private LiquidBowl[] bowls;

    public void OnBowlFillChanged()
    {
        if (IsDone())
        {
            foreach (Interactable interactable in inBowls)
            {
                interactable.SetActive(false);
            }
            anim.Play();
        }
    }

    private bool IsDone()
    {
        for (int i = 0; i < 10; i++)
        {
            if (sequence[i] != bowls[i].filled)
            {
                return false;
            }
        }

        return true;
    }
}
