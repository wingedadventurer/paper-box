using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    public DataItem data;

    [HideInInspector] public UnityEvent Picked;

    public void OnInteracted()
    {
        if (!Inventory.instance.GetEquippedItem())
        {
            Inventory.instance.AddItem(data);
            Picked.Invoke();
            Destroy(gameObject);
        }
    }

    public void SetInteractable(bool value)
    {
        foreach (Interactable interactable in GetComponentsInChildren<Interactable>())
        {
            //interactable.SetActive(value);
            interactable.SetInteractable(value);
        }
    }
}
