using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternGrid : MonoBehaviour
{
    [SerializeField] private Animation anim;
    [SerializeField] private bool[] correctPattern;
    [SerializeField] private GameButton[] buttons;
    [SerializeField] private Material matNormal;
    [SerializeField] private Material matPressed;

    private void Start()
    {
        foreach (GameButton button in buttons)
        {
            MeshRenderer mr = button.GetComponent<MeshRenderer>();
            Material[] mats = mr.sharedMaterials;
            mats[0] = matNormal;
            mr.sharedMaterials = mats;
        }
    }

    public void OnButtonPressed(int index)
    {
        MeshRenderer mr1 = buttons[index].GetComponent<MeshRenderer>();
        Material[] mats1 = mr1.sharedMaterials;
        mats1[0] = mats1[0] == matNormal ? matPressed : matNormal;
        mr1.sharedMaterials = mats1;

        if (IsCompleted())
        {
            anim.Play();
            foreach (GameButton button in buttons)
            {
                MeshRenderer mr2 = button.GetComponent<MeshRenderer>();
                Material[] mats2 = mr2.sharedMaterials;
                mats2[0] = matPressed;
                mr2.sharedMaterials = mats2;

                button.SetInteractable(false);
                button.toggle = true;
                button.SetPressed(true, true);
            }
        }
    }

    private bool IsCompleted()
    {
        for (int i = 0; i < 16; i++)
        {
            if ((buttons[i].GetComponent<MeshRenderer>().sharedMaterials[0] == matPressed) != correctPattern[i])
            {
                return false;
            }
        }

        return true;
    }
}
