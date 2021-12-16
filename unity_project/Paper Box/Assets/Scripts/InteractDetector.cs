using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractDetector : MonoBehaviour
{
    public Material matIdle, matHighlighted, matSelected;

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
            selected = true;
            meshRenderer.material = matSelected;
        }
    }

    public void OnInteractEnd()
    {
        selected = false;
        meshRenderer.material = highlighted ? matHighlighted : matIdle;
    }
}
