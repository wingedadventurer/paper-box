using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameButton : MonoBehaviour
{
    public bool pressed;
    public bool toggle;
    public Vector3 offset;
    [SerializeField] private Transform tr;
    [SerializeField] private Interactable interactable;
    [SerializeField] private bool addListener;

    private Vector3 posStart;
    private Vector3 posTarget;

    public UnityEvent Pressed;

    private void Start()
    {
        if (!tr)
        {
            tr = transform;
        }

        posStart = tr.localPosition;
        posTarget = posStart;

        if (addListener)
        {
            interactable.AddListener(DoPress);
        }
    }

    private void Update()
    {
        tr.localPosition = Vector3.Lerp(tr.localPosition, posTarget, 0.1f);

        if (pressed && !toggle && (tr.localPosition - posTarget).magnitude <= 0.00005f)
        {
            posTarget = posStart;
            pressed = false;
        }
    }

    public void SetPressed(bool value, bool skipEvent = false)
    {
        pressed = value;

        if (pressed)
        {
            posTarget = posStart + offset;
            if (!skipEvent)
            {
                Pressed.Invoke();
            }
        }
        else
        {
            posTarget = posStart;
        }
    }

    public void SetInteractable(bool value)
    {
        interactable.gameObject.SetActive(value);
    }

    public void DoPress()
    {
        if (pressed) { return; }

        SetPressed(true);
    }
}
