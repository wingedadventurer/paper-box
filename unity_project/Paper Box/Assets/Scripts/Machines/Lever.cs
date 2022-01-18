using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public GameObject goLeverHandle;
    public Interactable interactablePlace;
    public Interactable interactableUse;
    public Animation anim;
    public AnimationClip acPlace, acUse;
    public DataItem itemRequired;

    void Start()
    {
        goLeverHandle.SetActive(false);
        interactablePlace.gameObject.SetActive(true);
        interactableUse.gameObject.SetActive(false);

        interactablePlace.AddListener(OnInteractablePlace);
        interactableUse.AddListener(OnInteractableUse);

        anim.AddClip(acPlace, acPlace.name);
        anim.AddClip(acUse, acUse.name);
    }

    public void OnInteractablePlace()
    {

        if (Inventory.instance.GetEquippedItem() == itemRequired)
        {
            Inventory.instance.ConsumeEquippedItem();
            goLeverHandle.SetActive(true);
            interactablePlace.gameObject.SetActive(false);
            interactableUse.gameObject.SetActive(true);
            anim.Play(acPlace.name);
        }
    }

    public void OnInteractableUse()
    {
        interactableUse.gameObject.SetActive(false);
        anim.Play(acUse.name);
    }
}
