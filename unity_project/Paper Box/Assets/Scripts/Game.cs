using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public GameObject ui;
    public GameObject panelInventory, panelPause;
    public UIInventory uiInventory;

    public Image imageSelectedItem;

    [HideInInspector] public DataItem dataItemSelected;

    private bool paused;

    private Crosshair crosshair;

    void Start()
    {
        ui.SetActive(true);
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

    public void OnItemCollected(DataItem dataItem)
    {
        uiInventory.AddItem(dataItem);
        crosshair.OnInteractSuccess();
    }

    public void OnInventoryItemSelected(DataItem dataItem)
    {
        HideInventory();

        dataItemSelected = dataItem;

        crosshair.SetEquipped(true);

        // show item in game panel
        imageSelectedItem.sprite = dataItem.sprite;
        imageSelectedItem.enabled = true;
    }

    public void DeleteAndClearSelectedItem()
    {
        uiInventory.DeleteItem(dataItemSelected);
        ClearSelectedItem();
    }

    public void ClearSelectedItem()
    {
        dataItemSelected = null;
        imageSelectedItem.sprite = null;
        imageSelectedItem.enabled = false;
        crosshair.SetEquipped(false);
    }

    public void OnInteractSuccess()
    {
        DeleteAndClearSelectedItem();
        crosshair.OnInteractSuccess();
    }

    public void OnInteractFail()
    {
        crosshair.OnInteractFail();
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
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}
