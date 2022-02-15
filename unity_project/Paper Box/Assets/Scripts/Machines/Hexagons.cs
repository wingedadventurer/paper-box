using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexagons : MonoBehaviour
{
    public Animation anim;
    public int[] sequence = new int[6];
    public HexagonsHexagon[] hexagons;

    public void OnSpinned(int index)
    {
        if (IsCompleted())
        {
            foreach (HexagonsHexagon hexagon in hexagons)
            {
                hexagon.SetInteractable(false);
            }
            anim.Play();
        }
    }

    private bool IsCompleted()
    {
        for (int i = 0; i < 6; i++)
        {
            if (hexagons[i].n != sequence[i]) { return false; }
        }

        return true;
    }
}
