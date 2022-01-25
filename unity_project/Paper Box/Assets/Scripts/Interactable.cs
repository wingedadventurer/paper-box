using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField] private bool hideOnRun = true;

    public UnityEvent Interacted;

    private void Start()
    {
        if (hideOnRun)
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
    }

    public void AddListener(UnityAction call)
    {
        Interacted.AddListener(call);
    }

    public void Interact()
    {
        Interacted.Invoke();
    }

    public void SetActive(bool value)
    {
        GetComponent<Collider>().enabled = value;
    }
}
