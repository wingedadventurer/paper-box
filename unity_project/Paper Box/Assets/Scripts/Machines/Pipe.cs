using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public GameObject goValve;
    public Animation anim;
    public GameObject goIDPlace, goIDUse;
    public DataItem requiredItem;

    public AnimationClip acPlace, acUse;

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

        anim.AddClip(acPlace, "Place");
        anim.AddClip(acUse, "Use");
    }

    public void OnIDPlace()
    {
        if (game.dataItemSelected && game.dataItemSelected == requiredItem)
        {
            goValve.SetActive(true);
            goIDPlace.SetActive(false);
            goIDUse.SetActive(true);
            anim.Play("Place");
            game.DeleteSelectedItem();
            game.ClearSelectedItem();
        }
    }

    public void OnIDUse()
    {
        goIDUse.SetActive(false);
        anim.Play("Use");
    }
}
