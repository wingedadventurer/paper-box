using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewRack : MonoBehaviour
{
    private const int SCREW_COUNT = 6;

    [SerializeField] private Animation anim;
    [SerializeField] private GameObject prefabCrossScrew;
    [SerializeField] private GameObject prefabHexScrew;
    [SerializeField] private DataItem dataCrossScrew;

    [SerializeField] private DataItem[] datas;
    [SerializeField] private GameObject[] containers;
    [SerializeField] private Interactable[] interactablesPlace;

    private int countPlacedScrews;
    private Inventory inventory;

    private DataItem[] datasPlaced = new DataItem[SCREW_COUNT];

    private void Start()
    {
        inventory = Inventory.instance;
    }

    public void OnPlace(int indexPlace)
    {
        // get and consume item
        DataItem data = inventory.GetEquippedItem();
        inventory.ConsumeEquippedItem();

        // save data
        datasPlaced[indexPlace] = data;

        // spawn screw
        GameObject screw = Instantiate(data == dataCrossScrew ? prefabCrossScrew : prefabHexScrew);
        screw.transform.SetParent(containers[indexPlace].transform, true);
        screw.transform.localPosition = Vector3.zero;
        screw.transform.localEulerAngles = new Vector3(90, 0, 0);

        screw.GetComponent<Item>().Picked.AddListener(delegate { OnTake(indexPlace); });

        // update place interactable
        interactablesPlace[indexPlace].SetActive(false);

        countPlacedScrews++;

        // check if done
        if (countPlacedScrews == SCREW_COUNT && IsCompleted())
        {
            foreach (GameObject go in containers)
            {
                foreach (Interactable interactable in go.GetComponentsInChildren<Interactable>())
                {
                    interactable.SetActive(false);
                }
            }

            anim.Play();
        }
    }

    public void OnTake(int indexPlace)
    {
        datasPlaced[indexPlace] = null;
        countPlacedScrews--;

        // update place interactable
        interactablesPlace[indexPlace].SetActive(true);
    }

    private bool IsCompleted()
    {
        for(int i = 0; i < SCREW_COUNT; i++)
        {
            if (datas[i] != datasPlaced[i])
            {
                return false;
            }
        }

        return true;
    }
}
