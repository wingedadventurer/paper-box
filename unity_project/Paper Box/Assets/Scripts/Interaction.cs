using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    const float DETECT_DISTANCE = 1.0f;

    ClickDetector lastClickDetector;

    int layerInteraction;

    //private void Start()
    //{
        //layerInteraction = LayerMask.GetMask("Interaction");
    //}

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit raycastHit;
        //Physics.Raycast(ray, out raycastHit, DETECT_DISTANCE, layerInteraction);
        Physics.Raycast(ray, out raycastHit, DETECT_DISTANCE);
        if (raycastHit.collider)
        {
            GameObject go = raycastHit.collider.gameObject;
            if (go.tag == "ClickDetector")
            {
                ClickDetector clickDetector = go.GetComponent<ClickDetector>();
                if (lastClickDetector != clickDetector)
                {
                    lastClickDetector = clickDetector;
                    
                }
            }
            else
            {

            }
        }
    }
}
