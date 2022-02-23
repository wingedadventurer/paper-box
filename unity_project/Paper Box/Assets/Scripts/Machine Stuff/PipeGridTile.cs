using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PipeGridTile : MonoBehaviour
{
    public Interactable interactable;

    private Animation anim;
    private Transform trPipe;

    [HideInInspector] public float animPosOffset;
    [HideInInspector] public float animRotation;
    [HideInInspector] public int rot;

    private bool rotating;
    private Vector3 posStart;
    private float angle;

    public UnityEvent RotationDone;

    private void Start()
    {
        anim = GetComponent<Animation>();
        trPipe = transform;

        posStart = transform.localPosition;
    }

    private void Update()
    {
        if (rotating)
        {
            transform.localPosition = new Vector3(posStart.x, posStart.y + animPosOffset, posStart.z);
            angle = -animRotation + (rot - 1) * 90.0f;
            //transform.localEulerAngles = new Vector3(0, animRotation, 0);

            if (!anim.isPlaying)
            {
                rotating = false;
                transform.localPosition = posStart;

                animPosOffset = 0;
                animRotation = 0;

                //transform.Rotate(Vector3.up, 90, Space.Self);
                //trPipe.Rotate(Vector3.up, -90, Space.Self);

                interactable.SetActive(true);

                RotationDone.Invoke();
            }
        }

        transform.localEulerAngles = new Vector3(0, -angle, 0);
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
