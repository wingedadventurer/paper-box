using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    public Animation anim;
    public DataItem data;
    public bool[] sequence;
    public Interactable[] INs;
    public Interactable[] INsSliders;
    public GameObject[] sliderBones;
    public BridgeSlider[] sliders;

    void Start()
    {
        INs[0].SetActive(false);
        INs[1].SetActive(false);
        sliderBones[2].SetActive(false);
        INs[3].SetActive(false);
        sliderBones[4].SetActive(false);
        INs[5].SetActive(false);
    }

    public void OnSliderPlace(int i)
    {
        if (Inventory.instance.GetEquippedItem() == data)
        {
            Inventory.instance.ConsumeEquippedItem();
            INs[i].SetActive(false);
            sliderBones[i].SetActive(true);
        }
    }

    public void OnSliderPull()
    {
        if (IsSequenceCorrect())
        {
            foreach (Interactable interactable in INsSliders)
            {
                interactable.SetActive(false);
            }
            anim.Play();
        }
    }

    private bool IsSequenceCorrect()
    {
        for (int i = 0; i < 6; i++)
        {
            if (sliders[i].pulled != sequence[i])
            {
                return false;
            }

        }
        return true;
    }
}
