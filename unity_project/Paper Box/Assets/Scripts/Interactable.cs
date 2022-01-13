using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField] private UnityEvent Interacted;

    public void AddListener(UnityAction call)
    {
        Interacted.AddListener(call);
    }

    public void Interact()
    {
        Interacted.Invoke();
    }
}
