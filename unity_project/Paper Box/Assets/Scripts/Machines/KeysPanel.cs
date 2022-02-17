using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeysPanel : MonoBehaviour
{
    [SerializeField] private DataItem[] pattern;
    [SerializeField] private DataItem[] keyDatas;
    [SerializeField] private GameObject[] keyPrefabs;
    [SerializeField] private Interactable[] interactables;

    private DataItem[] placed = new DataItem[192];

    public void OnKeyPlace(int index)
    {
        DataItem data = Inventory.instance.GetEquippedItem();
        for (int i = 0; i < keyDatas.Length; i++)
        {
            if (data == keyDatas[i])
            {
                GameObject item = Instantiate(keyPrefabs[i]);

                Debug.Log("placed " + item + " to slot " + index);

                item.transform.SetParent(this.transform, true);
                item.transform.position = interactables[index].transform.position;
                item.transform.localEulerAngles = new Vector3(0, -90, 0);
                item.GetComponent<Item>().Picked.AddListener( delegate { OnKeyRemove(index); } );
                interactables[index].SetActive(false);
                placed[index] = data;
                break;
            }
        }

        Inventory.instance.ConsumeEquippedItem();

        if (IsCompleted())
        {
            Debug.Log("done");
        }
    }

    public void OnKeyRemove(int index)
    {
        interactables[index].SetActive(true);
        placed[index] = null;

        Debug.Log("removed from " + index);

        if (IsCompleted())
        {
            Debug.Log("done");
        }
    }

    private bool IsCompleted()
    {
        for (int i = 0; i < placed.Length; i++)
        {
            if (i == 0)
            {
                Debug.Log("0 | " + placed[i] + " - " + pattern[i]);
            }

            if (placed[i] != pattern[i])
            {
                Debug.Log("failed at " + i);
                return false;
            }
        }

        return true;
    }
}
