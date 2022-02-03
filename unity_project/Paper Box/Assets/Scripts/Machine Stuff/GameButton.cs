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

    private Vector3 posStart;
    private Vector3 posTarget;

    public UnityEvent Pressed;

    private void Start()
    {
        posStart = tr.localPosition;
        posTarget = posStart;
    }

    private void Update()
    {
        //transform.localPosition = Vector3.Lerp(transform.localPosition, pressed ? posStart + offset : posStart, 0.05f);
        tr.localPosition = Vector3.Lerp(tr.localPosition, posTarget, 0.1f);

        if (tr.localPosition.magnitude - posTarget.magnitude <= 0.001f)
        {
            if (!pressed)
            {
                posTarget = posStart;
            }
        }
    }

    public void SetPressed(bool value)
    {
        pressed = value;

        if (pressed)
        {
            posTarget = posStart + offset;
            Pressed.Invoke();
        }
    }

    public void SetEnabled(bool value)
    {
        interactable.gameObject.SetActive(value);
    }
}
