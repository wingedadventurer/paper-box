using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public GameObject ui;
    public GameObject panelInventory, panelPause;

    public Image imageSelectedItem;

    [HideInInspector] public DataItem dataItemSelected;

    private bool paused;

    private Crosshair crosshair;

    public static Game instance;

    private void Awake()
    {
        instance = this;

        ui.SetActive(true);
    }

    void Start()
    {
        panelInventory.SetActive(false);
        panelPause.SetActive(false);
        imageSelectedItem.enabled = false;
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
                if (dataItemSelected)
                {
                    ClearSelectedItem();
                    crosshair.SetEquipped(false);
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

    public void OnItemCollected(DataItem data)
    {
        Inventory.instance.AddItem(data);
    }

    public void OnInventoryItemSelected(DataItem data)
    {
        HideInventory();

        dataItemSelected = data;

        crosshair.SetEquipped(true);

        // show item in game panel
        imageSelectedItem.sprite = data.sprite;
        imageSelectedItem.enabled = true;
    }

    public void DeleteAndClearSelectedItem()
    {
        Inventory.instance.DeleteItem(dataItemSelected);
        ClearSelectedItem();
    }

    public void ClearSelectedItem() // TODO: move to Inventory
    {
        dataItemSelected = null;
        imageSelectedItem.sprite = null;
        imageSelectedItem.enabled = false;
        crosshair.SetEquipped(false);
    }

    public void OnInteractSuccess() // TODO: move to Interacting
    {
        DeleteAndClearSelectedItem();
    }

    public void OnInteractFail() // TODO: move to Interacting
    {

    }

    private void SetPaused(bool value)
    {
        paused = value;

        foreach (Movement o in FindObjectsOfType<Movement>())
        {
            o.active = !paused;
        }
        foreach (MouseLook o in FindObjectsOfType<MouseLook>())
        {
            o.active = !paused;
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
