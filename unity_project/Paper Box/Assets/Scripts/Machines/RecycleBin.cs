using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecycleBin : MonoBehaviour
{
    [SerializeField] private Interactable[] interactables;
    [SerializeField] private GameObject[] goLids;
    private bool[] openeds = new bool[5];
    private int countDone;

    private void Start()
    {
        openeds[4] = true;
    }

    private void Update()
    {
        // -90 open | 0 closed
        for (int i = 0; i < 5; i++)
        {
            Transform tr = goLids[i].transform;
            tr.localEulerAngles = new Vector3(
                    //Mathf.Lerp(tr.localEulerAngles.x, openeds[i] ? 0 : -90, 0.05f),
                    openeds[i] ? 0 : -90,
                    tr.localEulerAngles.y,
                    tr.localEulerAngles.z
            );
        }
    }

    public void OnInteracted(int index)
    {
        Inventory.instance.ConsumeEquippedItem();
        interactables[index].SetActive(false);
        openeds[index] = true;
        countDone++;

        if (countDone == 4)
        {
            openeds[4] = false;
            Debug.Log("done");
        }
    }
}
