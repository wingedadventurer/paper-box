using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{

    [SerializeField] private Animation anim;
    [SerializeField] private int[] sequence;
    [SerializeField] private DataItem[] datas;
    [SerializeField] private GameObject[] buttons;
    [SerializeField] private Interactable[] interactables;
    [SerializeField] private Interactable[] interactablesButtons;
    [SerializeField] private Transform[] positions;

    private List<int> entered;
    private bool completed;

    private void Start()
    {
        entered = new List<int>(6);
        for (int i = 0; i < 6; i++) { entered.Add(0); }

        for (int i = 0; i < 6; i++)
        {
            int a = i;
            buttons[i].SetActive(false);
            interactables[i].AddListener(delegate { OnButtonPlaced(a); });
            interactablesButtons[i].AddListener(delegate { OnButtonPressed(a); });
            interactables[i].gameObject.SetActive(true);
        }
    }

    private void OnButtonPlaced(int i)
    {
        Inventory inventory = Inventory.instance;
        if (inventory.GetEquippedItem() == datas[i])
        {
            inventory.ConsumeEquippedItem();
            interactables[i].gameObject.SetActive(false);
            buttons[i].SetActive(true);
        }
    }

    private void OnButtonPressed(int i)
    {
        Vector3 playerPos = Game.instance.GetPlayerPosition();
        Vector3 offset = playerPos - transform.position;

        transform.position = positions[i].position;
        PlayerTeleporter.instance.TeleportToPosition(transform.position + offset);

        entered.RemoveAt(0);
        entered.Add(i);

        if (!completed && IsCompleted())
        {
            completed = true;
            anim.Play();
        }
    }

    private bool IsCompleted()
    {
        for (int i = 0; i < 6; i++)
        {
            if (sequence[i] != entered[i])
            {
                return false;
            }
        }

        return true;
    }
}
