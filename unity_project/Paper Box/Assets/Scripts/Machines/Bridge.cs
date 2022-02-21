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
        sliderBones[1].SetActive(false);
        INs[2].SetActive(false);
        INs[3].SetActive(false);
        INs[4].SetActive(false);
        sliderBones[5].SetActive(false);
    }

    public void OnSliderPlace(int i)
    {
        if (Inventory.instance.GetEquippedItem() == data)
        {
            Inventory.instance.ConsumeEquippedItem();
            INs[i].SetActive(false);
            sliderBones[i].SetActive(true);
        }

        AudioManager.instance.PlaySFX(AudioManager.instance.sfxInsert);
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

        AudioManager.instance.PlaySFX(AudioManager.instance.sfxSwitch);
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
