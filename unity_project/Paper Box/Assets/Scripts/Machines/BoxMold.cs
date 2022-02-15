using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMold : MonoBehaviour
{
    [SerializeField] private Animation anim;
    [SerializeField] private GameObject goPiecesContainer;
    [SerializeField] private Interactable interactable;
    [SerializeField] private GameObject escapeBox;
    [SerializeField] private GameObject[] goPieces;

    private int n;

    public void OnPlace()
    {
        Inventory.instance.ConsumeEquippedItem();
        goPieces[n].SetActive(true);
        n++;
        if (n == 27)
        {
            interactable.SetActive(false);
            anim.Play();
            Invoke("OnMoldComplete", 1.6f);
        }
    }

    private void OnMoldComplete()
    {
        goPiecesContainer.SetActive(false);
        escapeBox.SetActive(true);
    }
}
