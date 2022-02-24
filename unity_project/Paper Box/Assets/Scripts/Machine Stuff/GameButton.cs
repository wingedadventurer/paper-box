using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameButton : MonoBehaviour
{
    public bool pressed;
    public bool toggle;
    public Vector3 offset;
    [SerializeField] private Transform tr;
    [SerializeField] private Interactable interactable;
    [SerializeField] private bool addListener;
    [SerializeField] private bool playPressSFX = true;

    private Vector3 posStart;
    private Vector3 posTarget;

    public UnityEvent Pressed;

    private void Start()
    {
        if (!tr)
        {
            tr = transform;
        }

        posStart = tr.localPosition;
        posTarget = posStart;

        if (addListener)
        {
            interactable.AddListener(DoPress);
        }
    }

    private void Update()
    {
        tr.localPosition = Vector3.Lerp(tr.localPosition, posTarget, 0.1f);

        if (pressed && !toggle && (tr.localPosition - posTarget).magnitude <= 0.005f)
        {
            posTarget = posStart;
            pressed = false;
        }
    }

    public void SetPressed(bool value, bool skipEvent = false, bool quiet = false)
    {
        pressed = value;

        if (pressed)
        {
            posTarget = posStart + offset;
            if (!skipEvent)
            {
                Pressed.Invoke();
            }

            if (playPressSFX && !quiet)
            {
                AudioManager.instance.PlaySFX(AudioManager.instance.sfxGameButton).SetPosition(transform.position).SetVolume(0.8f);
            }
        }
        else
        {
            posTarget = posStart;
        }
    }

    public void SetInteractable(bool value)
    {
        //interactable.gameObject.SetActive(value);
        interactable.SetInteractable(value);
    }

    public void DoPress()
    {
        if (pressed) { return; }

        SetPressed(true);
    }
}
