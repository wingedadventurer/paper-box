using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Setup")]
    public DataItem dataItemKey;

    [Header("Ref")]
    public Animation anim;
    public AnimationClip acOpen;
    public Interactable iLock;
    public GameObject goKey;

    private void Start()
    {
        anim.AddClip(acOpen, acOpen.name);

        iLock.AddListener(Open);
        goKey.SetActive(false);
    }

    public void Open()
    {
        if (Game.instance.dataItemSelected && Game.instance.dataItemSelected == dataItemKey)
        {
            iLock.gameObject.SetActive(false);
            anim.Play(acOpen.name);
            Game.instance.OnInteractSuccess();
            goKey.SetActive(true);
            Invoke("HideKey", 2);
        }
    }

    private void HideKey()
    {
        goKey.SetActive(false);
    }
}
