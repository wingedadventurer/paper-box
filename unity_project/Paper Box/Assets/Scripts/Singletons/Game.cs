using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public GameObject ui;
    public GameObject panelGame, panelInventory, panelPause;

    private bool paused;

    private Crosshair crosshair;
    private Inventory inventory;

    public static Game instance;

    private void Awake()
    {
        instance = this;

        ui.SetActive(true);
    }

    void Start()
    {
        inventory = Inventory.instance;

        panelInventory.SetActive(false);
        panelPause.SetActive(false);
        SetPaused(false);
        LockMouse(true);

        crosshair = FindObjectOfType<Crosshair>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetMouseButtonDown(1))
        {
            if (!panelPause.activeSelf)
            {
                if (inventory.GetEquippedItem())
                {
                    inventory.ClearEquippedItem();
                    crosshair.SetEquipped(false); // TODO: use events
                }
                else
                {
                    SetPaused(!paused);
                    LockMouse(!paused);
                    panelInventory.SetActive(paused);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetPaused(!paused);
            LockMouse(!paused);
            if (panelInventory.activeSelf)
            {
                panelInventory.SetActive(false);
            }
            else
            {
                panelPause.SetActive(paused);
            }
        }
    }

    public void OnInventoryItemSelected(DataItem data)
    {
        HideInventory();
        //crosshair.SetEquipped(true); // TODO: use events
    }

    private void SetPaused(bool value)
    {
        paused = value;

        panelGame.SetActive(!paused);

        foreach (Movement o in FindObjectsOfType<Movement>())
        {
            o.active = !paused;
        }
        foreach (MouseLook o in FindObjectsOfType<MouseLook>())
        {
            o.active = !paused;
        }
        foreach (Interacting o in FindObjectsOfType<Interacting>())
        {
            //o.active = !paused;
            o.enabled = !paused;
        }
    }

    private void LockMouse(bool value)
    {
        Cursor.lockState = value ? CursorLockMode.Locked : CursorLockMode.None;
    }

    public void HideInventory()
    {
        SetPaused(false);
        LockMouse(true);
        panelInventory.SetActive(false);
    }

    public void ResumeGame()
    {
        SetPaused(!paused);
        LockMouse(!paused);
        panelPause.SetActive(paused);
    }

    public void EndGame()
    {
        Scenes.instance.LoadMenu();
    }

    public void QuitGame()
    {
        Scenes.instance.QuitGame();
    }
}
