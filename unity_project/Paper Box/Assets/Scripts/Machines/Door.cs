using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Setup")]
    public GameObject prefabKey;

    [Header("Ref")]
    public Animation anim;
    public Interactable inLock;
    public GameObject goKeyPos;

    private DataItem dataKey;
    private GameObject goKey;

    private void Start()
    {
        if (prefabKey)
        {
            dataKey = prefabKey.GetComponent<Item>().data;
            inLock.requestedItems.Add(dataKey);
        }
    }

    public void Open()
    {
        Inventory.instance.ConsumeEquippedItem();
        inLock.gameObject.SetActive(false);
        anim.Play();
        Invoke("PlaceEnd", 1.75f);

        // spawn and add key
        goKey = Instantiate(prefabKey);
        goKey.transform.SetParent(goKeyPos.transform, true);
        goKey.transform.localPosition = Vector3.zero;
        goKey.transform.localEulerAngles = new Vector3(0, 0, -90);
        goKey.GetComponent<Item>().SetInteractable(false);
    }

    private void PlaceEnd()
    {
        // make key takeable again
        goKey.GetComponent<Item>().SetInteractable(true);
    }
}
