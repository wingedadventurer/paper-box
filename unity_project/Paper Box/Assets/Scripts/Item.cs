using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    public DataItem data;

    public UnityEvent Picked;

    public void OnInteracted()
    {
        if (!Inventory.instance.GetEquippedItem())
        {
            Inventory.instance.AddItem(data);
            Picked.Invoke();
            AudioSource sfx = AudioManager.instance.PlaySFX(AudioManager.instance.sfxPickup);
            sfx.volume = 0.6f;
            Destroy(gameObject);
        }
    }

    public void SetInteractable(bool value)
    {
        foreach (Interactable interactable in GetComponentsInChildren<Interactable>())
        {
            interactable.SetInteractable(value);
        }
    }
}
