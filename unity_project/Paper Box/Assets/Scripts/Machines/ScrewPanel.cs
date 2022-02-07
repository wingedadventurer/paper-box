using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewPanel : MonoBehaviour
{
    [SerializeField] private Animation anim;
    [SerializeField] private DataItem dataTool;
    [SerializeField] private DataItem dataScrew;
    [SerializeField] private Interactable interactable;
    [SerializeField] private GameObject[] screws;

    int countScrewsToRemove = 3;

    public void OnInteract()
    {
        Inventory inventory = Inventory.instance;
        if (inventory.GetEquippedItem() == dataTool)
        {
            interactable.SetActive(false);
            anim.Play();
            Invoke("RemoveScrew", 0.33f);
        }
    }

    private void RemoveScrew()
    {
        countScrewsToRemove--;
        screws[countScrewsToRemove].SetActive(false);
        Inventory inventory = Inventory.instance;
        inventory.AddItem(dataScrew);
        if (countScrewsToRemove > 0)
        {
            Invoke("RemoveScrew", 0.33f);
        }
    }
}
