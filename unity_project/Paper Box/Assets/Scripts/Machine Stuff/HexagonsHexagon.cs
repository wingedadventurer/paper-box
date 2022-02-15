using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HexagonsHexagon : MonoBehaviour
{
    [SerializeField] private Interactable interactable;

    public int n;

    public UnityEvent Spinned;

    private void Update()
    {
        transform.localEulerAngles = new Vector3(n * 60.0f, 0, 0);

        // wtf
        //transform.localEulerAngles = new Vector3(Mathf.Lerp(transform.localEulerAngles.x, targetAngle, 0.05f), 0, 0);
    }

    public void SetInteractable(bool value)
    {
        interactable.SetActive(value);
    }

    public void Spin()
    {
        n++;
        if (n == 6)
        {
            n = 0;
        }

        Spinned.Invoke();
    }
}
