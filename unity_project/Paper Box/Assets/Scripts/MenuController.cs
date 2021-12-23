using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject panelMain, panelSettings, panelCredits;

    private void Start()
    {
        // initialize panels
        panelMain.SetActive(true);
        panelSettings.SetActive(false);
        panelCredits.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}
