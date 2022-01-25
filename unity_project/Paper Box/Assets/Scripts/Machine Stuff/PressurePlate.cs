using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private bool releaseOnExit;

    private bool pressed;

    public UnityEvent Pressed;

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

        transform.position = new Vector3(transform.position.x, -0.075f, transform.position.z);
        pressed = true;
        Pressed.Invoke();
    }

    public void Release()
    {
        if (!pressed) { return; }

        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        pressed = false;
    }
}
