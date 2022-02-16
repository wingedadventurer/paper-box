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

        // initial
        SwapPillars(pillars[0], pillars[2]);
        SwapPillars(pillars[1], pillars[5]);
        SwapPillars(pillars[6], pillars[7]);
        SwapPillars(pillars[3], pillars[8]);
    }

    public void OnButtonPressed(int i)
    {
        if (indexButtonPressed > -1)
        {
            // button already pressed

            if (indexButtonPressed == i)
            {
                // same button pressed

                return;
            }
            else
            {
                // 2nd button pressed

                SwapPillars(pillars[i], pillars[indexButtonPressed]);

                buttons[indexButtonPressed].toggle = false;
                buttons[indexButtonPressed].SetInteractable(true);

                indexButtonPressed = -1;

                if (IsSequenceCorrect())
                {
                    foreach (GameButton button in buttons)
                    {
                        button.SetInteractable(false);
                        button.toggle = true;
                        button.SetPressed(true, true);
                    }

                    anim.Play();
                }
            }
        }
        else
        {
            // first time press

            buttons[i].toggle = true;
            buttons[i].SetInteractable(false);
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
