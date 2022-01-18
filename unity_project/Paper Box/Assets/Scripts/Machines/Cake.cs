using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cake : MonoBehaviour
{
    [SerializeField] private DataItem dataItemCandle;
    [SerializeField] private DataItem dataItemLighter;
    [SerializeField] private GameObject[] goCandles;
    [SerializeField] private Interactable[] iCandleSlots;
    [SerializeField] private Interactable[] iCandles;

    private int countLitCandles;
    private Inventory inventory;

    private void Awake()
    {
        for(int i = 0; i < 6; i++)
        {
            goCandles[i].SetActive(false);
        }
    }

    private void Start()
    {
        inventory = Inventory.instance;
    }

    public void OnCandleSlotInteracted(int index)
    {
        if (inventory.GetEquippedItem() == dataItemCandle)
        {
            inventory.ConsumeEquippedItem();
            goCandles[index].SetActive(true);
            iCandleSlots[index].gameObject.SetActive(false);
        }
    }

    public void OnCandleInteracted(int index)
    {
        if (inventory.GetEquippedItem() == dataItemLighter)
        {
            iCandles[index].gameObject.SetActive(false);
            countLitCandles++;

            if (countLitCandles == 6)
            {
                // TODO: completed
                Debug.Log("done");
            }
        }
    }
}
