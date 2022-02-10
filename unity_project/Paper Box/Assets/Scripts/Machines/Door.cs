using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Setup")]
    public DataItem dataKey;
    public GameObject prefabKey;

    [Header("Ref")]
    public Animation anim;
    public Interactable inLock;
    public GameObject goKeyPos;

    private GameObject goKey;

    private void Start()
    {
        inLock.requestedItems.Add(dataKey);
    }

    public void Open()
    {
        if (Inventory.instance.GetEquippedItem() == dataKey)
        {
            Inventory.instance.ClearEquippedItem();
            inLock.gameObject.SetActive(false);
            anim.Play();
            Invoke("HideKey", 2);

            // spawn and add key
            goKey = Instantiate(prefabKey);
            goKey.transform.SetParent(goKeyPos.transform, true);
            goKey.transform.localPosition = Vector3.zero;
            goKey.transform.localEulerAngles = new Vector3(0, 0, -90);
            foreach (Interactable interactable in goKey.GetComponentsInChildren<Interactable>())
            {
                interactable.SetActive(false);
            }
        }
    }

    private void HideKey()
    {
        Destroy(goKey);
    }
}
