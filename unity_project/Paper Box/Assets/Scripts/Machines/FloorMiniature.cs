using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMiniature : MonoBehaviour
{
    [SerializeField] private GameObject goMiniature;
    [SerializeField] private GameObject goMaxiature;
    [SerializeField] private Interactable intPlace;

    private void Start()
    {
        goMaxiature.SetActive(false);
    }

    public void OnPlace()
    {
        Inventory.instance.ConsumeEquippedItem();
        intPlace.SetActive(false);
        goMiniature.SetActive(true);
        goMaxiature.SetActive(true);
    }
}
