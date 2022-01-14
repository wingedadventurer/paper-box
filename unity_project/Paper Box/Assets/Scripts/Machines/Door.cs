using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animation anim;
    public AnimationClip acOpen;
    public Interactable interactableKey;
    public DataItem requiredItem;
    public GameObject goKey;

    private void Start()
    {
        anim.AddClip(acOpen, acOpen.name);

        interactableKey.AddListener(Open);
        goKey.SetActive(false);
    }

    public void Open()
    {
        if (Game.instance.dataItemSelected && Game.instance.dataItemSelected == requiredItem)
        {
            interactableKey.gameObject.SetActive(false);
            anim.Play(acOpen.name);
            Game.instance.OnInteractSuccess();
            goKey.SetActive(true);
            Invoke("ShowKey", 2);
        }
    }

    private void ShowKey()
    {
        goKey.SetActive(true);
    }
}
