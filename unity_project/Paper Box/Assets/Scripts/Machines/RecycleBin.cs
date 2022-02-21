using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecycleBin : MonoBehaviour
{
    private const int BIN_COUNT = 5;

    [SerializeField] private Interactable[] interactables;
    [SerializeField] private GameObject[] goLids;
    private bool[] openeds = new bool[BIN_COUNT];
    private float[] angles = new float[BIN_COUNT];
    private int countDone;

    private void Start()
    {
        openeds[4] = true;
    }

    private void Update()
    {
        // -90 open | 0 closed
        for (int i = 0; i < BIN_COUNT; i++)
        {
            angles[i] = Mathf.LerpAngle(angles[i], openeds[i] ? 0 : -90, 0.08f);
            Transform tr = goLids[i].transform;
            tr.localEulerAngles = new Vector3(
                    angles[i],
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
