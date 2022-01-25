using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private bool releaseOnExit;

    const float PRESS_AMOUNT = 0.075f;

    private bool pressed;

    public UnityEvent Pressed;

    private float posYStart;

    private void Start()
    {
        posYStart = transform.position.y;
    }

    private void Update()
    {
        Vector3 posTarget = pressed ? new Vector3(transform.position.x, posYStart - PRESS_AMOUNT, transform.position.z) : new Vector3(transform.position.x, posYStart, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, posTarget, 0.05f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!pressed && other.gameObject.tag == "Player")
        {
            Press();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (releaseOnExit && pressed && other.gameObject.tag == "Player")
        {
            Release();
        }
    }

    public void Press()
    {
        if (pressed) { return; }

        Pressed.Invoke();
        pressed = true;
    }

    public void Release()
    {
        if (!pressed) { return; }

        pressed = false;
    }
}
