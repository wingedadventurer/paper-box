using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeGrid : MonoBehaviour
{
    public Animation anim;
    public PipeGridTile[] tiles;
    public Interactable[] INs;

    public void OnPipeInteracted(int index)
    {
        tiles[index].Rotate();
        if (IsSolved())
        {
            foreach (Interactable interactable in INs)
            {
                interactable.SetActive(false);
            }
        }
    }

    public void OnTileRotationDone()
    {
        if (IsSolved())
        {
            foreach (Interactable interactable in INs)
            {
                interactable.SetActive(false);
            }
            anim.Play();
        }
    }

    private bool IsSolved()
    {
        if (
            (tiles[12].rot == 0 || tiles[12].rot == 2) &&
            (tiles[13].rot == 2) &&
            (tiles[5].rot == 1) &&
            (tiles[4].rot == 0) &&
            (tiles[8].rot == 3) &&
            (tiles[10].rot == 0 || tiles[10].rot == 2) &&
            (tiles[11].rot == 2) &&
            (tiles[7].rot == 1 || tiles[7].rot == 3) &&
            (tiles[3].rot == 0)
            //(tiles[].rot == ) &&
        )
        {
            return true;
        }
        return false;
    }
}
