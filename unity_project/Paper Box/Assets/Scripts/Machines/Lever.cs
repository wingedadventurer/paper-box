using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lever : MonoBehaviour
{
    public GameObject goLeverHandle;
    public GameObject goIDPlace, goIDUse;
    public Animation anim;
    public AnimationClip acPlace, acUse;
    public DataItem requiredItem;

    public UnityEvent Activated;

    private Game game;

    private void Awake()
    {
        game = Game.instance;
    }

    void Start()
    {
        goLeverHandle.SetActive(false);
        goIDPlace.SetActive(true);
        goIDUse.SetActive(false);

        anim.AddClip(acPlace, acPlace.name);
        anim.AddClip(acUse, acUse.name);
    }

    public void OnIDPlace()
    {
        if (game.dataItemSelected && game.dataItemSelected == requiredItem)
        {
            goLeverHandle.SetActive(true);
            goIDPlace.SetActive(false);
            goIDUse.SetActive(true);
            anim.Play(acPlace.name);
            game.OnInteractSuccess();
        }
        else
        {
            game.OnInteractFail();
        }
    }

    public void OnIDUse()
    {
        goIDUse.SetActive(false);
        anim.Play(acUse.name);
        Activated.Invoke();
        game.OnInteractSuccess();
    }
}
