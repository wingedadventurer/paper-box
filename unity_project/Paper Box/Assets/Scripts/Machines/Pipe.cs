using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public GameObject goValve;
    public Interactable interactablePlace, interactableUse;
    public Animation anim;
    public AnimationClip acPlace, acUse;
    public DataItem requiredItem;

    private Game game;

    private void Awake()
    {
        game = Game.instance;

        interactablePlace.AddListener(OnIDPlace);
        interactableUse.AddListener(OnIDUse);
    }

    void Start()
    {
        goValve.SetActive(false);
        interactablePlace.enabled = true;
        interactableUse.enabled = false;

        anim.AddClip(acPlace, acPlace.name);
        anim.AddClip(acUse, acUse.name);
    }

    public void OnIDPlace()
    {
        if (game.dataItemSelected && game.dataItemSelected == requiredItem)
        {
            goValve.SetActive(true);
            interactablePlace.enabled = false;
            interactableUse.enabled = true;
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
        interactableUse.enabled = false;
        anim.Play(acUse.name);
        game.OnInteractSuccess();
    }
}
