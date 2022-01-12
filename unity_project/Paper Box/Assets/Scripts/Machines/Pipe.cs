using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pipe : MonoBehaviour
{
    public GameObject goValve;
    public InteractDetector idPlace, idUse;
    public Animation anim;
    public AnimationClip acPlace, acUse;
    public DataItem requiredItem;

    public UnityEvent Activated;

    private Game game;

    private void Awake()
    {
        game = FindObjectOfType<Game>();

        idPlace.Interacted.AddListener(OnIDPlace);
        idUse.Interacted.AddListener(OnIDUse);
    }

    void Start()
    {
        

        goValve.SetActive(false);
        idPlace.enabled = true;
        idUse.enabled = false;

        anim.AddClip(acPlace, acPlace.name);
        anim.AddClip(acUse, acUse.name);
    }

    public void OnIDPlace()
    {
        if (game.dataItemSelected && game.dataItemSelected == requiredItem)
        {
            goValve.SetActive(true);
            idPlace.enabled = false;
            idUse.enabled = true;
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
        idUse.enabled = false;
        anim.Play(acUse.name);
        Activated.Invoke();
        game.OnInteractSuccess();
    }
}
