using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public GameObject goValve;
    public GameObject goIDPlace, goIDUse;
    public Animation anim;
    public AnimationClip acPlace, acUse;
    public DataItem requiredItem;

    private Game game;

    private void Awake()
    {
        game = FindObjectOfType<Game>();
    }

    void Start()
    {
        goValve.SetActive(false);
        goIDPlace.SetActive(true);
        goIDUse.SetActive(false);

        anim.AddClip(acPlace, acPlace.name);
        anim.AddClip(acUse, acUse.name);
    }

    public void OnIDPlace()
    {
        if (game.dataItemSelected && game.dataItemSelected == requiredItem)
        {
            goValve.SetActive(true);
            goIDPlace.SetActive(false);
            goIDUse.SetActive(true);
            anim.Play(acPlace.name);
            game.DeleteSelectedItem();
            game.ClearSelectedItem();
        }
    }

    public void OnIDUse()
    {
        goIDUse.SetActive(false);
        anim.Play(acUse.name);
    }
}
