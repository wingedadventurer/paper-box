using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolRack : MonoBehaviour
{
    [SerializeField] private Animation anim;
    [SerializeField] private DataItem[] toolDatas;
    [SerializeField] private GameObject[] tools;
    [SerializeField] private Interactable[] interactablesPlace;
    [SerializeField] private Interactable[] interactablesTake;

    private int countPlacedTools;
    private Inventory inventory;

    private void Start()
    {
        inventory = Inventory.instance;
    }

    public void OnToolPlace(int index)
    {
        if (inventory.HasEquippedItem(toolDatas[index]))
        {
            inventory.ConsumeEquippedItem();
            countPlacedTools++;
            interactablesPlace[index].SetActive(false);
            tools[index].SetActive(true);
            if (countPlacedTools == 4)
            {
                foreach (Interactable interactable in interactablesTake)
                {
                    interactable.SetActive(false);
                }
                anim.Play();
            }
        }
    }

    public void OnToolTake(int index)
    {
        inventory.AddItem(toolDatas[index]);
        interactablesPlace[index].SetActive(true);
        tools[index].SetActive(false);
        countPlacedTools--;
    }
}
