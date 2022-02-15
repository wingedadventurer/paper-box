using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    [SerializeField] private DataItem data;

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
}
