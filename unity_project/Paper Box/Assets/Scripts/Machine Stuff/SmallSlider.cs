using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallSlider : MonoBehaviour
{
    public bool slid;
    public Vector3 offset;

    private Vector3 posStart;

    private void Start()
    {
        posStart = transform.localPosition;
    }

    private void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, slid ? posStart + offset : posStart, 0.05f);
    }

    public void SetSlid(bool value)
    {
        slid = value;
        //transform.localPosition = slid ? posStart + offset : posStart;
    }

    public void Slide()
    {
        slid = !slid;
        //transform.localPosition = slid ? posStart + offset : posStart;
    }
}
