using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtPot : MonoBehaviour
{
    [SerializeField] private DataItem dataShovel;
    [SerializeField] private GameObject goDirtFull;
    [SerializeField] private GameObject goDirtDug;
    [SerializeField] private GameObject goDirtSide;
    [SerializeField] private Interactable interactable;

    private void Start()
    {
        goDirtSide.SetActive(false);
    }

    public void OnInteract()
    {
        if (Inventory.instance.HasEquippedItem(dataShovel))
        {
            goDirtFull.SetActive(false);
            goDirtSide.SetActive(true);
            interactable.SetActive(false);
        }
    }
}
