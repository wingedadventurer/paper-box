using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PipeGridTile : MonoBehaviour
{
    public Animation anim;
    public Interactable interactable;
    public Transform trPipe;

    public float animPosOffset;
    public float animRotation;

    public int rot;

    private bool rotating;

    private Vector3 posStart;

    public UnityEvent RotationDone;

    private void Start()
    {
        posStart = transform.localPosition;
    }

    private void Update()
    {
        if (rotating)
        {
            transform.localPosition = new Vector3(posStart.x, posStart.y + animPosOffset, posStart.z);
            transform.localEulerAngles = new Vector3(0, animRotation, 0);

            if(!anim.isPlaying)
            {
                rotating = false;
                transform.localPosition = posStart;

                transform.Rotate(Vector3.up, 90, Space.Self);
                trPipe.Rotate(Vector3.up, -90, Space.Self);

                interactable.SetActive(true);

                RotationDone.Invoke();
            }
        }
    }

    public void Rotate()
    {
        if (rotating) { return; }

        interactable.SetActive(false);
        anim.Play();
        rotating = true;
        rot++;
        if (rot > 3) { rot = 0; }
    }
}
