using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScatteredButtons : MonoBehaviour
{
    public Animation anim;
    public Material matGreen;
    public GameButton[] buttons;
    public MeshRenderer[] mrs;

    private int count;

    public void OnButtonPressed(int index)
    {
        Debug.Log("A");

        mrs[index].sharedMaterial = matGreen;
        buttons[index].toggle = true;   
        buttons[index].SetInteractable(false);

        count++;
        if (count == 10)
        {
            anim.Play();
        }
    }
}
