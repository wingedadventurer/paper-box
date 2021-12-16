using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    const float DETECT_DISTANCE = 1.5f;

    InteractDetector lastClickDetector;

    bool interacting;
    bool interactingLast;

    private void Update()
    {
        // update interact input
        interacting = Input.GetMouseButton(0) || Input.GetKey(KeyCode.E);
    }
    
    private void FixedUpdate()
    {
        // check if interact changed
        if (interactingLast != interacting)
        {
            interactingLast = interacting;
            if (interacting)
            {
                if (lastClickDetector)
                {
                    lastClickDetector.OnInteractStart();
                }
            }
            else
            {
                if (lastClickDetector)
                {
                    lastClickDetector.OnInteractEnd();
                }
            }
        }

        // update click detector
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit raycastHit;
        Physics.Raycast(ray, out raycastHit, DETECT_DISTANCE);
        if (raycastHit.collider)
        {
            GameObject go = raycastHit.collider.gameObject;
            if (go.tag == "InteractDetector")
            {
                InteractDetector clickDetector = go.GetComponent<InteractDetector>();
                if (lastClickDetector != clickDetector)
                {
                    if (lastClickDetector)
                    {
                        lastClickDetector.OnInteractExit();
                    }
                    lastClickDetector = clickDetector;
                    lastClickDetector.OnInteractEnter();
                }
            }
            else
            {
                if (lastClickDetector)
                {
                    lastClickDetector.OnInteractExit();
                    lastClickDetector = null;
                }
            }
        }
        else
        {
            if (lastClickDetector)
            {
                lastClickDetector.OnInteractExit();
                lastClickDetector = null;
            }
        }
    }
}
