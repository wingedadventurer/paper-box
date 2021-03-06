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

    const float DETECT_DISTANCE = 1.75f;
    const float DETECT_DISTANCE_DOWN = 2.5f;

    private Interactable lastInteractable;

    private bool interacting;
    private bool interactingLast;

    public static Interacting instance;

    public Text textDebug;

    private int mask;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        mask = ~(1 << LayerMask.NameToLayer("SolidTransparent"));
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
                    lastInteractable = null;
                }
                else
                {
                    
                }
            }
        }
        // get ray from camera
        Camera cam = Camera.main;
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit raycastHit;

        // calculate interact ray length
        float rayLength = DETECT_DISTANCE;
        float angle = Vector3.Angle(Vector3.down, cam.transform.forward);
        float angleClamped = Mathf.Clamp(angle, 0, 180);
        rayLength += (DETECT_DISTANCE_DOWN - DETECT_DISTANCE) * (1 - Mathf.Sin(Mathf.Deg2Rad * angleClamped));

        // get new interactable
        Interactable newInteractable = null;
        Physics.Raycast(ray, out raycastHit, rayLength, mask);
        if (raycastHit.collider)
        {
            GameObject go = raycastHit.collider.gameObject;

            if (go.TryGetComponent(out Interactable interactable))
            {
                if (interactable.interactable)
                {
                    Inventory inventory = Inventory.instance;
                    if (interactable.requestedItems.Count == 0)
                    {
                        if (inventory.GetEquippedItem() == null)
                        {
                            newInteractable = interactable;
                        }
                    }
                    else
                    {
                        if (interactable.requestedItems.Contains(inventory.GetEquippedItem()))
                        {
                            newInteractable = interactable;
                        }
                    }
                }
            }
        }

        // update old interactable
        if (newInteractable)
        {
            if (lastInteractable != newInteractable)
            {
                lastInteractable = newInteractable;
                Entered.Invoke();
            }
        }
        else
        {
            lastInteractable = null;
            Exited.Invoke();
        }
    }
}
