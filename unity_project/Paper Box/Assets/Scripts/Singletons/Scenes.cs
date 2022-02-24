using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    [SerializeField] private Image imageShade;

    private const int SCENE_INDEX_MENU = 0;
    private const int SCENE_INDEX_GAME = 1;
    private const float FADE_SPEED = 1.5f;

    private int sceneIndexToLoad = -1;

    private float shadeCurrent;
    private float shadeTarget;

    public static Scenes instance;

    private void Awake()
    {
        if (instance) { return; }
        DontDestroyOnLoad(gameObject);
        instance = this;
    }

    private void Start()
    {
        shadeCurrent = 1;
        shadeTarget = 0;
        SetBlocking(false);
    }

    private void Update()
    {
        // update shade value
        if (!Mathf.Approximately(shadeCurrent, shadeTarget))
        {
            shadeCurrent = Mathf.MoveTowards(shadeCurrent, shadeTarget, FADE_SPEED * Time.unscaledDeltaTime);
            if (Mathf.Approximately(shadeCurrent, shadeTarget))
            {
                // faded out
                if (sceneIndexToLoad != -1)
                {
                    StartCoroutine(LoadScene());
                    //SceneManager.LoadScene(sceneIndexToLoad);
                }
            }
        }

        // update shade color
        Color c = imageShade.color;
        c.a = shadeCurrent;
        imageShade.color = c;
    }

    public void LoadGame()
    {
        sceneIndexToLoad = SCENE_INDEX_GAME;
        SetBlocking(true);
        FadeOut();
    }

    public void LoadMenu()
    {
        sceneIndexToLoad = SCENE_INDEX_MENU;
        SetBlocking(true);
        FadeOut();
    }

    public void QuitGame()
    {
        Debug.Log("quit");

        Application.Quit();
    }

    public void FadeIn()
    {
        shadeCurrent = 1;
        shadeTarget = 0;
    }

    public void FadeOut()
    {
        shadeCurrent = 0;
        shadeTarget = 1;
    }

    public void SetBlocking(bool value)
    {
        imageShade.raycastTarget = value;
    }

    private IEnumerator LoadScene()
    {
        yield return SceneManager.LoadSceneAsync(sceneIndexToLoad);
        Time.timeScale = 1;
        FadeIn();
        SetBlocking(false);
        sceneIndexToLoad = -1;
    }
}
