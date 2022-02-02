using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightKnobs : MonoBehaviour
{
    public Animation anim;
    public bool[] patternInitial;
    public bool[] patternRequired;
    public Interactable[] INs;
    public SmallSlider[] sliders;

    private void Start()
    {
        for (int i = 0; i < 8; i++)
        {
            sliders[i].SetSlid(patternInitial[i]);
        }
    }

    public void OnSliderInteract(int i)
    {
        // slide the slider + adjacent ones
        switch (i)
        {
            case 0:
                sliders[0].Slide();
                sliders[1].Slide();
                sliders[3].Slide();
                break;
            case 1:
                sliders[0].Slide();
                sliders[1].Slide();
                sliders[2].Slide();
                break;
            case 2:
                sliders[1].Slide();
                sliders[2].Slide();
                sliders[4].Slide();
                break;
            case 3:
                sliders[0].Slide();
                sliders[3].Slide();
                sliders[5].Slide();
                break;
            case 4:
                sliders[2].Slide();
                sliders[4].Slide();
                sliders[7].Slide();
                break;
            case 5:
                sliders[3].Slide();
                sliders[5].Slide();
                sliders[6].Slide();
                break;
            case 6:
                sliders[5].Slide();
                sliders[6].Slide();
                sliders[7].Slide();
                break;
            case 7:
                sliders[4].Slide();
                sliders[6].Slide();
                sliders[7].Slide();
                break;
        }

        if (IsPatternCorrect())
        {
            foreach (Interactable interactable in INs)
            {
                interactable.SetActive(false);
            }
            anim.Play();
        }
    }

    private bool IsPatternCorrect()
    {
        // TODO
        for(int i = 0; i < 8; i++)
        {
            if (sliders[i].slid != patternRequired[i])
            {
                return false;
            }
        }

        return true;
    }
}
