using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public Panel panelMain, panelSettings, panelCredits;

    private void Start()
    {
        // initialize panels
        panelMain.SetVisible(false, true);
        panelSettings.SetVisible(false, true);
        panelCredits.SetVisible(false, true);

        AudioManager.instance.PlayMusic(AudioManager.instance.musicMenu);

        Invoke("ShowMainPanel", 1.0f);
    }

    public void ShowMainPanel()
    {
        panelMain.SetVisible(true, false);
    }

    public void StartGame()
    {
        panelMain.SetVisible(false);
        Scenes.instance.LoadGame();
    }

    public void QuitGame()
    {
        panelMain.SetVisible(false);
        Scenes.instance.QuitGame();
    }
}
