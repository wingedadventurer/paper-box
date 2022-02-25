using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScatteredButtons : MonoBehaviour
{
    public Animation anim;
    public Material matOn;
    public GameButton[] buttons;
    public MeshRenderer[] mrLeds;

    private int count;

    public void OnButtonPressed(int index)
    {
        Material[] mats = mrLeds[index].sharedMaterials;
        mats[1] = matOn;
        mrLeds[index].sharedMaterials = mats;
        buttons[index].SetInteractable(false);

        count++;
        if (count == 10)
        {
            anim.Play();
        }
    }
}
