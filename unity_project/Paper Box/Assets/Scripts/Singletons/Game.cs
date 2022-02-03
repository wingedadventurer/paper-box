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
    private Interacting interacting;

    [SerializeField] private GameObject goPlayer;

    public static Game instance;

    private void Awake()
    {
        instance = this;

        ui.SetActive(true);
    }

    void Start()
    {
        inventory = Inventory.instance;
        crosshair = FindObjectOfType<Crosshair>();
        interacting = Interacting.instance;

        panelInventory.SetActive(false);
        panelPause.SetActive(false);
        SetPaused(false);
        LockMouse(true);


        // connect Inventory to Crosshair
        inventory.Equipped.AddListener( delegate { crosshair.SetEquipped(true); });
        inventory.Unequipped.AddListener( delegate { crosshair.SetEquipped(false); });

        // connect Interacting to Crosshair
        interacting.Entered.AddListener(delegate { crosshair.SetCanInteract(true); });
        interacting.Exited.AddListener(delegate { crosshair.SetCanInteract(false); });
        interacting.Interacted.AddListener(crosshair.OnInteract);
    }

    void Update()
    {
        // INVENTORY
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetMouseButtonDown(1))
        {
            if (!panelPause.activeSelf)
            {
                if (inventory.GetEquippedItem())
                {
                    inventory.ClearEquippedItem();
                    //crosshair.SetEquipped(false); // TODO: use events
                }
                else
                {
                    SetPaused(!paused);
                    LockMouse(!paused);
                    panelInventory.SetActive(paused);
                }
            }
        }

        // DEBUG: TELEPORTING
        if (Input.GetKeyDown(KeyCode.Alpha1)) { PlayerTeleporter.instance.TeleportToRoom(0); }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { PlayerTeleporter.instance.TeleportToRoom(1); }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { PlayerTeleporter.instance.TeleportToRoom(2); }
        if (Input.GetKeyDown(KeyCode.Alpha4)) { PlayerTeleporter.instance.TeleportToRoom(3); }
        if (Input.GetKeyDown(KeyCode.Alpha5)) { PlayerTeleporter.instance.TeleportToRoom(4); }
        if (Input.GetKeyDown(KeyCode.Alpha6)) { PlayerTeleporter.instance.TeleportToRoom(5); }

        // PAUSE
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
    }

    private void SetPaused(bool value)
    {
        paused = value;

        panelGame.SetActive(!paused);

        foreach (Movement o in FindObjectsOfType<Movement>())
        {
            o.active = !paused;
        }
        foreach (Looking o in FindObjectsOfType<Looking>())
        {
            o.active = !paused;
        }
        foreach (Interacting o in FindObjectsOfType<Interacting>())
        {
            //o.active = !paused;
            o.enabled = !paused;
        }

        Time.timeScale = paused ? 0 : 1;
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

    public Vector3 GetPlayerPosition()
    {
        return goPlayer.transform.position;
    }
}
