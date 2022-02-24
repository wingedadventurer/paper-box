using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HexagonsHexagon : MonoBehaviour
{
    [SerializeField] private Interactable interactable;

    public int n;

    public UnityEvent Spinned;

    private float angle;

    private void Update()
    {
        //transform.localEulerAngles = new Vector3(n * 60.0f, 0, 0);

        // wtf
        angle = Mathf.LerpAngle(angle, n * 60.0f, 0.05f);
        transform.localEulerAngles = new Vector3(angle, 0, 0);
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

        AudioManager.instance.PlaySFX(AudioManager.instance.sfxTurn).SetVolume(0.6f);

        Spinned.Invoke();
    }
}
