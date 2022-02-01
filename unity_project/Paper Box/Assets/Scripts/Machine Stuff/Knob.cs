using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Knob : MonoBehaviour
{
    public UnityEvent Turned;

    public int dir;

    public void Turn()
    {
        dir++;
        if (dir == 6)
        {
            dir = 0;
        }

        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, dir * 60);

        Turned.Invoke();
    }
}
