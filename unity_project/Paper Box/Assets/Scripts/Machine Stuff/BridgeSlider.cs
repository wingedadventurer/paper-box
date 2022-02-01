using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeSlider : MonoBehaviour
{
    public bool pulled;

    public void Toggle()
    {
        Animation anim = GetComponent<Animation>();
        anim.Play(pulled ? "SliderPullUp" : "SliderPullDown");
        pulled = !pulled;
    }
}
