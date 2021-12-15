using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public GameObject panelInventory, panelPause;

    private bool paused;

    void Start()
    {
        panelInventory.SetActive(false);
        panelPause.SetActive(false);
        SetPaused(false);
        LockMouse(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!panelPause.activeSelf)
            {
                SetPaused(!paused);
                LockMouse(!paused);
                panelInventory.SetActive(paused);
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
