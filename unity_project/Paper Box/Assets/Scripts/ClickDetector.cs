using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickDetector : MonoBehaviour
{
    const float DETECT_DISTANCE = 1.0f;

    public Material matIdle, matEnter, matHighlighted, matSelected;

    private Transform transformPlayer;
    private MeshRenderer meshRenderer;
    private bool checking;
    private bool highlighted;
    private bool selected;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        meshRenderer = GetComponent<MeshRenderer>();
        if (player)
        {
            transformPlayer = player.transform;
        }
    }

    private void Start()
    {
        meshRenderer.material = matIdle;
    }

    private void OnMouseOver()
    {
        if (checking)
        {
            highlighted = Vector3.Distance(transform.position, transformPlayer.position) <= DETECT_DISTANCE;
            if (selected)
            {
                if (!highlighted)
                {
                    selected = false;
                }
            }
            else
            {
                meshRenderer.material = highlighted ? matHighlighted : matEnter;
            }
        }
    }

    private void OnMouseEnter()
    {
        checking = true;
        meshRenderer.material = matEnter;
    }

    private void OnMouseExit()
    {
        checking = false;
        highlighted = false;
        selected = false;
        meshRenderer.material = matIdle;
    }

    private void OnMouseDown()
    {
        if (highlighted)
        {
            selected = true;
            meshRenderer.material = matSelected;
        }
    }

    private void OnMouseUp()
    {
        selected = false;
    }
}
