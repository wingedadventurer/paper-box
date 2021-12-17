using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractDetector : MonoBehaviour
{
    public Material matIdle, matHighlighted, matSelected;
    public UnityEvent Interacted;

    private MeshRenderer meshRenderer;
    private bool highlighted;
    private bool selected;


    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        meshRenderer.material = matIdle;
    }

    public void OnInteractEnter()
    {
        highlighted = true;
        meshRenderer.material = matHighlighted;
    }

    public void OnInteractExit()
    {
        highlighted = false;
        selected = false;
        meshRenderer.material = matIdle;
    }

    public void OnInteractStart()
    {
        if (highlighted)
        {
            if (!selected)
            {
                Interacted.Invoke();
                selected = true;
            }
            meshRenderer.material = matSelected;
        }
    }

    public void OnInteractEnd()
    {
        selected = false;
        meshRenderer.material = highlighted ? matHighlighted : matIdle;
    }
}