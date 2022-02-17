using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//[System.Serializable]
//public class ItemEvent : UnityEvent<Item> {}

public class Item : MonoBehaviour
{
    public DataItem data;
    //public ItemEvent Picked;

    public UnityEvent Picked;
    

    public void OnInteracted()
    {
        if (!Inventory.instance.GetEquippedItem())
        {
            Inventory.instance.AddItem(data);
            //Picked.Invoke(this);
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
