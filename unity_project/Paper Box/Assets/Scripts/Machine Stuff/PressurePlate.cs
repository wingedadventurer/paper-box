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

    private void OnTriggerEnter(Collider other)
    {
        if (!pressed && other.gameObject.tag == "Player")
        {
            Debug.Log("player in " + gameObject.name);
            Press();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (releaseOnExit && pressed && other.gameObject.tag == "Player")
        {
            Debug.Log("player out " + gameObject.name);
            Release();
        }
    }

    public void Press()
    {
        if (pressed) { return; }

        transform.position = new Vector3(transform.position.x, posYStart - PRESS_AMOUNT, transform.position.z);
        pressed = true;
        Pressed.Invoke();
    }

    public void Release()
    {
        if (!pressed) { return; }

        transform.position = new Vector3(transform.position.x, posYStart, transform.position.z);
        pressed = false;
    }
}
