using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameButton : MonoBehaviour
{
    public bool pressed;
    public bool toggle;
    public Vector3 offset;

    private Vector3 posStart;
    private Vector3 posTarget;

    private void Start()
    {
        posStart = transform.localPosition;
        posTarget = posStart;
    }

    private void Update()
    {
        //transform.localPosition = Vector3.Lerp(transform.localPosition, pressed ? posStart + offset : posStart, 0.05f);
        transform.localPosition = Vector3.Lerp(transform.localPosition, posTarget, 0.1f);

        if (transform.localPosition.magnitude - posTarget.magnitude <= 0.001f)
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
        }
    }
}
