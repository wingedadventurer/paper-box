using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity;

    [Header("Ref")]
    public Transform transformHead;

    private float mouseOffsetY;

    void Update()
    {
        // get look input
        Vector2 lookInput = Vector2.zero;
        lookInput.x = Input.GetAxis("Mouse X");
        lookInput.y = Input.GetAxis("Mouse Y");

        // apply look x input
        transform.Rotate(Vector3.up, lookInput.x * mouseSensitivity);

        // apply look y input
        mouseOffsetY = Mathf.Clamp(mouseOffsetY + lookInput.y * mouseSensitivity, -85.0f, 85.0f);
        transformHead.localEulerAngles = new Vector3(-mouseOffsetY, 0, 0);
    }
}
