using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plates : MonoBehaviour
{
    [SerializeField] private PressurePlate[] plates;
    [SerializeField] private PressurePlate[] plateSequence;
    [SerializeField] private Interactable iEnd;
    [SerializeField] private Interactable iRetry;

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

        iEnd.AddListener(OnEndInteract);
        iRetry.AddListener(OnRetryInteract);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Retry();
        }
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
        iEnd.gameObject.SetActive(false);

        if (succeeded)
        {
            iRetry.gameObject.SetActive(false);
            Debug.Log("success!");
        }
        else
        {
            Debug.Log("fail!");
        }

        foreach (PressurePlate plate in plates)
        {
            plate.Press();
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
        iEnd.gameObject.SetActive(true);

        Debug.Log("retry");
    }
}
