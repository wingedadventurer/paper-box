using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BridgeSlider : MonoBehaviour
{
    public bool pulled;

    public Animation anim;

    public UnityEvent Pulled;

    public void Toggle()
    {
        pulled = !pulled;
        anim.Play(pulled ? "SliderPullDown" : "SliderPullUp");
        Pulled.Invoke();
    }
}
