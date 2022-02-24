using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public GameObject goValve;
    public Interactable interactablePlace, interactableUse;
    public Animation anim;
    public AnimationClip acPlace, acUse;
    public DataItem requiredItem;

    private void Awake()
    {
        interactablePlace.AddListener(OnInteractablePlace);
        interactableUse.AddListener(OnInteractableUse);
    }

    void Start()
    {
        goValve.SetActive(false);
        interactablePlace.gameObject.SetActive(true);
        interactableUse.gameObject.SetActive(false);

        anim.AddClip(acPlace, acPlace.name);
        anim.AddClip(acUse, acUse.name);
    }

    public void OnInteractablePlace()
    {
        Inventory.instance.ConsumeEquippedItem();
        goValve.SetActive(true);
        interactablePlace.gameObject.SetActive(false);
        interactableUse.gameObject.SetActive(true);
        anim.Play(acPlace.name);

        AudioManager.instance.PlaySFX(AudioManager.instance.sfxInsert);
    }

    public void OnInteractableUse()
    {
        interactableUse.gameObject.SetActive(false);
        anim.Play(acUse.name);

        AudioManager.instance.PlaySFX(AudioManager.instance.sfxTurn);
        AudioManager.instance.PlaySFX(AudioManager.instance.sfxSmolDoor).SetDelay(0.5f);
    }
}
