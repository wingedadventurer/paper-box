using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractDetector : MonoBehaviour
{
    public UnityEvent Interacted;

    private MeshRenderer meshRenderer;
    private bool highlighted;
    private bool selected;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void OnInteractEnter()
    {
        highlighted = true;
    }

    public void OnInteractExit()
    {
        highlighted = false;
        selected = false;
    }

    public void OnInteractStart()
    {
        if (enabled && highlighted && !selected)
        {
            selected = true;
            Interacted.Invoke();
        }
    }

    public void OnInteractEnd()
    {
        selected = false;
    }
}
