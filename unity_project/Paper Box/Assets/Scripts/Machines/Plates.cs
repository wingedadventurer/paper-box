using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plates : MonoBehaviour
{
    [SerializeField] private Animation anim;
    [SerializeField] private GameButton buttonEnd;
    [SerializeField] private GameButton buttonRetry;
    [SerializeField] private PressurePlate[] plates;
    [SerializeField] private PressurePlate[] plateSequence;

    private int indexCurrent;
    private bool failed;
    private bool finished;
    private bool succeeded;

    void Start()
    {
        foreach (PressurePlate plate in plates)
        {
            plate.Pressed.AddListener( delegate { OnPlatePressed(plate); } );
        }

        buttonRetry.SetPressed(true, true);
        buttonRetry.SetInteractable(false);
    }

    private void OnPlatePressed(PressurePlate plate)
    {
        if (succeeded || failed)
        {
            return;
        }

        if (plate == plateSequence[indexCurrent])
        {
            indexCurrent++;
            if (indexCurrent == plateSequence.Length)
            {
                succeeded = true;
            }
        }
        else
        {
            failed = true;
        }
    }

    public void OnEndInteract()
    {
        buttonEnd.SetInteractable(false);

        if (succeeded)
        {
            // success

            foreach (PressurePlate plate in plates)
            {
                plate.Press();
            }

            anim.Play();
        }
        else
        {
            // fail

            buttonRetry.SetInteractable(true);
            buttonRetry.SetPressed(false);

            foreach (PressurePlate plate in plates)
            {
                plate.Press();
                plate.SetGlowing(false);
            }
        }
    }

    public void OnRetryInteract()
    {
        Retry();
    }

    private void Retry()
    {
        foreach (PressurePlate plate in plates)
        {
            plate.Release();
        }

        indexCurrent = 0;
        failed = false;
        succeeded = false;

        buttonRetry.SetPressed(true, true);
        buttonRetry.SetInteractable(false);
        buttonEnd.SetPressed(false);
        buttonEnd.SetInteractable(true);
    }
}
