using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animation anim;
    public AnimationClip acOpen;

    private bool opened;

    private void Start()
    {
        anim.AddClip(acOpen, acOpen.name);
    }

    public void Open()
    {
        if (!opened)
        {
            opened = true;
            anim.Play(acOpen.name);
        }
    }
}
