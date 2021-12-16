using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public GameObject ui;
    public GameObject panelInventory, panelPause;
    public UIInventory uiInventory;

    public Image imageSelectedItem;

    public DataItem dataItemSelected;

    private bool paused;

    void Start()
    {
        ui.SetActive(true);
        panelInventory.SetActive(false);
        panelPause.SetActive(false);
        imageSelectedItem.enabled = false;
        SetPaused(false);
        LockMouse(true);
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

    public void OnItemCollected(DataItem dataItem)
    {
        uiInventory.AddItem(dataItem);
    }

    public void OnInventoryItemSelected(DataItem dataItem)
    {
        SetPaused(false);
        LockMouse(true);
        panelInventory.SetActive(false);

        dataItemSelected = dataItem;

        // show item in game panel
        imageSelectedItem.sprite = dataItem.sprite;
        imageSelectedItem.enabled = true;
    }

    public void DeleteSelectedItem()
    {
        uiInventory.DeleteItem(dataItemSelected);
        dataItemSelected = null;
        imageSelectedItem.sprite = null;
        imageSelectedItem.enabled = false;
    }

    public void ClearSelectedItem()
    {
        dataItemSelected = null;
        imageSelectedItem.sprite = null;
        imageSelectedItem.enabled = false;
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

        if (paused)
        {
            
        }
        else
        {

        }
    }

    private void LockMouse(bool value)
    {
        Cursor.lockState = value ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
