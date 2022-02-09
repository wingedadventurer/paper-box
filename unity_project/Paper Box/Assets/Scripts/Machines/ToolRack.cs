using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolRack : MonoBehaviour
{
    [SerializeField] private Animation anim;

    [Header("Map data to tool")]
    [SerializeField] private DataItem[] datas;
    [SerializeField] private GameObject[] tools;

    [Header("...")]
    [SerializeField] private Interactable[] interactablesPlace;
    [SerializeField] private Interactable[] interactablesTake;

    private int countPlacedTools;
    private Inventory inventory;

    private DataItem[] datasPlaced = new DataItem[4];
    private Vector3[] posOriginal = new Vector3[4];
    private int[] indexes = new int[4];

    private void Start()
    {
        inventory = Inventory.instance;
        for (int i = 0; i < 4; i++)
        {
            indexes[i] = -1;
            posOriginal[i] = tools[i].transform.position;
        }
    }

    public void OnToolPlace(int indexPlace)
    {
        // get item
        DataItem data = inventory.GetEquippedItem();
        inventory.ConsumeEquippedItem();

        // save data
        datasPlaced[indexPlace] = data;

        // save indexes
        for (int i = 0; i < 4; i++)
        {
            if (data == datas[i])
            {
                indexes[indexPlace] = i;
                break;
            }
        }

        // enable and position tool
        tools[indexes[indexPlace]].SetActive(true);
        tools[indexes[indexPlace]].transform.position = posOriginal[indexPlace];

        countPlacedTools++;

        // update place interactable
        interactablesPlace[indexPlace].SetActive(false);

        // check if done
        if (countPlacedTools == 4 && IsCompleted())
        {
            foreach (Interactable interactable in interactablesTake)
            {
                interactable.SetActive(false);
            }
            anim.Play();
        }
    }

    public void OnToolTake(int indexTool)
    {
        int indexPlace = -1;

        for (int i = 0; i < 4; i++)
        {
            if (indexes[i] == indexTool)
            {
                indexPlace = i;
                break;
            }
        }

        tools[indexTool].SetActive(false);
        inventory.AddItem(datas[indexTool]);

        countPlacedTools--;

        // update place interactable
        interactablesPlace[indexPlace].SetActive(true);
    }

    private bool IsCompleted()
    {
        for(int i = 0; i < 4; i++)
        {
            if (indexes[i] != i)
            {
                return false;
            }
        }

        return true;
    }
}
