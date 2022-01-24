using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{

    [SerializeField] private DataItem[] datas;
    [SerializeField] private GameObject[] buttons;
    [SerializeField] private Interactable[] interactables;
    [SerializeField] private Interactable[] interactablesButtons;
    [SerializeField] private Transform[] positions;

    private void Start()
    {
        buttons[0].SetActive(true);
        buttons[1].SetActive(false);
        buttons[2].SetActive(false);
        buttons[3].SetActive(false);
        buttons[4].SetActive(false);
        buttons[5].SetActive(false);

        interactables[1].AddListener( delegate { OnButtonPlaced(1); } );
        interactables[2].AddListener( delegate { OnButtonPlaced(2); } );
        interactables[3].AddListener( delegate { OnButtonPlaced(3); } );
        interactables[4].AddListener( delegate { OnButtonPlaced(4); } );
        interactables[5].AddListener( delegate { OnButtonPlaced(5); } );

        interactablesButtons[0].AddListener(delegate { OnButtonPressed(0); });
        interactablesButtons[1].AddListener(delegate { OnButtonPressed(1); });
        interactablesButtons[2].AddListener(delegate { OnButtonPressed(2); });
        interactablesButtons[3].AddListener(delegate { OnButtonPressed(3); });
        interactablesButtons[4].AddListener(delegate { OnButtonPressed(4); });
        interactablesButtons[5].AddListener(delegate { OnButtonPressed(5); });

        interactables[0].gameObject.SetActive(false);
        interactables[1].gameObject.SetActive(true);
        interactables[2].gameObject.SetActive(true);
        interactables[3].gameObject.SetActive(true);
        interactables[4].gameObject.SetActive(true);
        interactables[5].gameObject.SetActive(true);
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
    }
}
