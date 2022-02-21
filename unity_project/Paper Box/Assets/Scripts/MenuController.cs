using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject panelMain, panelSettings, panelCredits;

    private void Start()
    {
        // initialize panels
        panelMain.SetActive(true);
        panelSettings.SetActive(false);
        panelCredits.SetActive(false);

        AudioManager.instance.PlayMusic(AudioManager.instance.musicMenu);
    }

    public void StartGame()
    {
        Scenes.instance.LoadGame();
    }

    public void QuitGame()
    {
        Scenes.instance.QuitGame();
    }
}
