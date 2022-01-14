using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Interacting : MonoBehaviour
{
    [HideInInspector] public UnityEvent Entered;
    [HideInInspector] public UnityEvent Exited;
    [HideInInspector] public UnityEvent Interacted;

    const float DETECT_DISTANCE = 1.25f;
    const float DETECT_DISTANCE_DOWN = 2.0f;

    private Interactable lastInteractable;

    private bool interacting;
    private bool interactingLast;

    public static Interacting instance;

    public Text t;

    private void Awake()
    {
        instance = this;
    }

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
                if (lastInteractable)
                {
                    lastInteractable.Interact();
                    Interacted.Invoke();
                    if (!lastInteractable || !lastInteractable.enabled)
                    {
                        Exited.Invoke();
                    }
                }
            }
        }

        // update interact detector
        Camera cam = Camera.main;
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit raycastHit;

        float rayLength = DETECT_DISTANCE;
        float angle = Vector3.Angle(Vector3.down, cam.transform.forward);
        float angleClamped = Mathf.Clamp(angle, 0, 90);
        rayLength += (DETECT_DISTANCE_DOWN - DETECT_DISTANCE) * Mathf.Cos(Mathf.Deg2Rad * angleClamped);
        
        Physics.Raycast(ray, out raycastHit, rayLength);
        if (raycastHit.collider)
        {
            GameObject go = raycastHit.collider.gameObject;

            if (go.TryGetComponent(out Interactable interactable))
            {
                if (interactable.enabled && lastInteractable != interactable)
                {
                    lastInteractable = interactable;
                    Entered.Invoke();
                }
            }
            else
            {
                if (lastInteractable)
                {
                    Exited.Invoke();
                    lastInteractable = null;
                }
            }
        }
        else
        {
            if (lastInteractable)
            {
                Exited.Invoke();
                lastInteractable = null;
            }
        }
    }
}
