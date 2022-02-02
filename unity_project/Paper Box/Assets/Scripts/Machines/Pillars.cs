using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillars : MonoBehaviour
{
    public Animation anim;
    public Interactable[] INButtons;
    public PillarsPillar[] pillars;
    public GameButton[] buttons;

    private int indexButtonPressed = -1;

    private void Start()
    {
        for(int i = 0; i < 10; i++)
        {
            pillars[i].SetHeight(i);
        }

        SwapPillars(pillars[0], pillars[2]);
        SwapPillars(pillars[1], pillars[5]);
        SwapPillars(pillars[6], pillars[7]);
        SwapPillars(pillars[3], pillars[8]);
    }

    public void OnButtonPressed(int i)
    {
        if (indexButtonPressed > -1)
        {
            if (indexButtonPressed == i)
            {
                buttons[i].SetPressed(false);
                indexButtonPressed = -1;
            }
            else
            {
                SwapPillars(pillars[i], pillars[indexButtonPressed]);

                buttons[i].SetPressed(false);
                buttons[indexButtonPressed].SetPressed(false);

                indexButtonPressed = -1;

                if (IsSequenceCorrect())
                {
                    foreach (Interactable interactable in INButtons)
                    {
                        interactable.SetActive(false);
                    }
                    anim.Play();
                }
            }
        }
        else
        {
            buttons[i].SetPressed(true);
            indexButtonPressed = i;
        }
    }

    private bool IsSequenceCorrect()
    {
        for (int i = 0; i < 10; i++)
        {
            if (pillars[i].height != i)
            {
                return false;
            }
        }
        return true;
    }

    private void SwapPillars(PillarsPillar a, PillarsPillar b)
    {
        int temp = a.height;
        a.SetHeight(b.height);
        b.SetHeight(temp);
    }
}
