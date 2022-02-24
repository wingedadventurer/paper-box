using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeysPanel : MonoBehaviour
{
    [SerializeField] private Animation anim;
    [SerializeField] private DataItem[] pattern;
    [SerializeField] private DataItem[] keyDatas;
    [SerializeField] private GameObject[] keyPrefabs;
    [SerializeField] private Interactable[] interactables;
    [SerializeField] private Item itemCube;

    private DataItem[] placed = new DataItem[192];

    private void Start()
    {
        for (int i = 0; i < interactables.Length; i++)
        {
            int t = i;
            interactables[i].AddListener( delegate { OnKeyPlace(t); } );
        }

        #region SETUP PATTERN
        int a = 0;
        pattern[18] = keyDatas[a++];
        pattern[28] = keyDatas[a++];
        pattern[10] = keyDatas[a++];
        pattern[29] = keyDatas[a++];
        pattern[16] = keyDatas[a++];

        pattern[51] = keyDatas[a++];
        pattern[59] = keyDatas[a++];
        pattern[40] = keyDatas[a++];
        pattern[35] = keyDatas[a++];
        pattern[38] = keyDatas[a++];

        pattern[76] = keyDatas[a++];
        pattern[75] = keyDatas[a++];
        pattern[66] = keyDatas[a++];
        pattern[77] = keyDatas[a++];
        pattern[68] = keyDatas[a++];

        pattern[122] = keyDatas[a++];
        pattern[101] = keyDatas[a++];
        pattern[121] = keyDatas[a++];
        pattern[111] = keyDatas[a++];
        pattern[113] = keyDatas[a++];

        pattern[151] = keyDatas[a++];
        pattern[149] = keyDatas[a++];
        pattern[148] = keyDatas[a++];
        pattern[152] = keyDatas[a++];
        pattern[129] = keyDatas[a++];

        pattern[190] = keyDatas[a++];
        pattern[174] = keyDatas[a++];
        pattern[182] = keyDatas[a++];
        pattern[169] = keyDatas[a++];
        pattern[167] = keyDatas[a++];
        #endregion
    }

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

        AudioManager.instance.PlaySFX(AudioManager.instance.sfxKeyInsert);

        if (IsCompleted())
        {
            OnDone();
        }
    }

    public void OnKeyRemove(int index)
    {
        interactables[index].SetActive(true);
        placed[index] = null;

        Debug.Log("removed from " + index);

        if (IsCompleted())
        {
            OnDone();
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

    private void OnDone()
    {
        foreach (Item item in GetComponentsInChildren<Item>())
        {
            item.SetInteractable(false);
        }

        itemCube.SetInteractable(true);

        anim.Play();
    }
}
