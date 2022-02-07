using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LiquidBowl : MonoBehaviour
{
    [SerializeField] private DataItem dataBottleEmpty;
    [SerializeField] private DataItem dataBottleFull;

    public bool filled;

    [SerializeField] private GameObject goWater;
    [SerializeField] private Interactable interactable;

    public UnityEvent FillChanged;

    private void Start()
    {
        goWater.SetActive(filled);
        interactable.requiredItem = filled ? dataBottleEmpty : dataBottleFull;
    }

    public void OnInteract()
    {
        Inventory inventory = Inventory.instance;
        if (filled)
        {
            if (inventory.GetEquippedItem() == dataBottleEmpty)
            {
                inventory.DeleteItem(dataBottleEmpty);
                inventory.AddItem(dataBottleFull);
                inventory.EquipItem(dataBottleFull);
                filled = false;
                goWater.SetActive(filled);
                interactable.requiredItem = filled ? dataBottleEmpty : dataBottleFull;
                FillChanged.Invoke();
            }
        }
        else
        {
            if (inventory.GetEquippedItem() == dataBottleFull)
            {
                inventory.DeleteItem(dataBottleFull);
                inventory.AddItem(dataBottleEmpty);
                inventory.EquipItem(dataBottleEmpty);
                //inventory.ClearEquippedItem();
                filled = true;
                goWater.SetActive(filled);
                interactable.requiredItem = filled ? dataBottleEmpty : dataBottleFull;
                FillChanged.Invoke();
            }
        }
        
    }
}
