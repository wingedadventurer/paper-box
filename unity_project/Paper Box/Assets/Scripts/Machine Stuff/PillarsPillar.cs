using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarsPillar : MonoBehaviour
{
    public Transform trRod;

    public int height;

    private float targetHeight;
    private bool animating;

    private void Start()
    {
        targetHeight = trRod.localPosition.z;
    }

    private void Update()
    {
        if (animating)
        {
            trRod.localPosition = new Vector3(0, 0, Mathf.Lerp(trRod.localPosition.z, targetHeight, 0.05f));
            if (Mathf.Approximately(trRod.localPosition.z, targetHeight))
            {
                animating = false;
            }
        }
    }

    public void SetHeight(int i)
    {
        height = i;
        animating = true;
        targetHeight = 0.001f + 0.0005f * height;
    }
}
