using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TangramFrame : MonoBehaviour
{
    [SerializeField] private Animation anim;
    [SerializeField] private GameObject[] pieces;
    [SerializeField] private Interactable[] insPlace;

    private int countPlaced;

    private void Start()
    {
        foreach (GameObject go in pieces)
        {
            go.SetActive(false);
        }
    }

    public void OnPiecePlace(int index)
    {
        Inventory.instance.ConsumeEquippedItem();
        insPlace[index].SetActive(false);
        pieces[index].SetActive(true);
        countPlaced++;
        if (countPlaced == 5)
        {
            anim.Play();
        }
    }
}
