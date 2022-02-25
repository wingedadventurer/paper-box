using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public bool debug;

    public GameObject ui;
    public Panel panelGame;
    public Panel panelInventory;
    public Panel panelPause;
    public Panel panelComplete;
    public Text textPlaytime;
    public Text textFPS;
    public Clock clock;

    private bool paused;

    private Crosshair crosshair;
    private Inventory inventory;
    private Interacting interacting;

    [SerializeField] private GameObject goPlayer;

    public static Game instance;

    private float playtime;

    private bool completed;

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

        panelInventory.SetVisible(false, true);
        panelPause.SetVisible(false, true);
        panelComplete.SetVisible(false, true);
        SetPaused(false);
        LockMouse(true);

        // connect Inventory to Crosshair
        inventory.Equipped.AddListener( delegate { crosshair.SetEquipped(true); });
        inventory.Unequipped.AddListener( delegate { crosshair.SetEquipped(false); });

        // connect Interacting to Crosshair
        interacting.Entered.AddListener(delegate { crosshair.SetCanInteract(true); });
        interacting.Exited.AddListener(delegate { crosshair.SetCanInteract(false); });
        interacting.Interacted.AddListener(crosshair.OnInteract);

        AudioManager.instance.PlayMusic(AudioManager.instance.musicGame);

        InvokeRepeating("UpdateFPSText", 0, 0.5f);
    }

    void Update()
    {
        // INVENTORY
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetMouseButtonDown(1) && !completed)
        {
            if (!panelPause.visible)
            {
                if (inventory.GetEquippedItem())
                {
                    inventory.ClearEquippedItem();
                }
                else
                {
                    SetPaused(!paused);
                    LockMouse(!paused);
                    panelInventory.SetVisible(paused);
                }
            }
        }

        // DEBUG: TELEPORTING
        if (debug)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) { PlayerTeleporter.instance.TeleportToRoom(0); }
            if (Input.GetKeyDown(KeyCode.Alpha2)) { PlayerTeleporter.instance.TeleportToRoom(1); }
            if (Input.GetKeyDown(KeyCode.Alpha3)) { PlayerTeleporter.instance.TeleportToRoom(2); }
            if (Input.GetKeyDown(KeyCode.Alpha4)) { PlayerTeleporter.instance.TeleportToRoom(3); }
            if (Input.GetKeyDown(KeyCode.Alpha5)) { PlayerTeleporter.instance.TeleportToRoom(4); }
            if (Input.GetKeyDown(KeyCode.Alpha6)) { PlayerTeleporter.instance.TeleportToRoom(5); }
        }

        // PAUSE
        if (Input.GetKeyDown(KeyCode.Escape) && !completed)
        {
            SetPaused(!paused);
            LockMouse(!paused);
            if (panelInventory.visible)
            {
                panelInventory.SetVisible(false);
            }
            else
            {
                panelPause.SetVisible(paused);
            }
        }

        playtime += Time.deltaTime;

        clock.SetTime(playtime);
    }

    public void OnInventoryItemSelected(DataItem data)
    {
        HideInventory();
    }

    private void SetPaused(bool value)
    {
        paused = value;

        panelGame.SetVisible(!paused);

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
        panelInventory.SetVisible(false);
    }

    public void ResumeGame()
    {
        SetPaused(!paused);
        LockMouse(!paused);
        panelPause.SetVisible(paused);
    }

    public void CompleteGame()
    {
        completed = true;
        SetPaused(true);
        LockMouse(false);
        panelComplete.SetVisible(true);
        panelGame.SetVisible(false);

        int seconds = (int)(playtime);
        int hours = seconds / 3600;
        seconds %= 3600;
        int minutes = seconds / 60;
        seconds %= 60;

        string s = "";
        if (hours > 0)
        {
            s += hours.ToString() + "h ";
            s += minutes.ToString() + "m ";
        }
        else if (minutes > 0)
        {
            s += minutes.ToString() + "m ";
        }
        s += seconds.ToString() + "s";

        textPlaytime.text = "TIME\n" + s;
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

    private void UpdateFPSText()
    {
        textFPS.text = "FPS: " + (int)(1f / Time.unscaledDeltaTime);
    }
}
