using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedButtons : MonoBehaviour
{
    const float BUTTON_DURATION = 60; // [s]

    [SerializeField] private Animation anim;
    [SerializeField] private GameButton[] buttons;
    [SerializeField] private GameObject[] bars;

    private float[] times = new float[6];
    private bool active;

    private void Start()
    {
        active = true;
    }

    private void Update()
    {
        if (active)
        {
            for (int i = 0; i < 6; i++)
            {
                if (times[i] > 0)
                {
                    times[i] -= Time.deltaTime;
                    if (times[i] <= 0)
                    {
                        buttons[i].toggle = false;
                        buttons[i].SetInteractable(true);
                    }
                }
            }
        }

        // update visuals
        for (int i = 0; i < 6; i++)
        {
            float n = Mathf.Lerp(bars[i].transform.localScale.z, times[i] / BUTTON_DURATION, 0.03f);
            bars[i].transform.localScale = new Vector3(1, 1, n);
        }
    }

    public void OnButtonPressed(int index)
    {
        times[index] = BUTTON_DURATION;
        buttons[index].toggle = true;
        buttons[index].SetInteractable(false);

        // check if completed
        if (IsCompleted())
        {
            active = false;
            for (int i = 0; i < 6; i++)
            {
                times[i] = BUTTON_DURATION;
            }
            anim.Play();
        }
    }

    private bool IsCompleted()
    {
        foreach (float time in times)
        {
            if (time <= 0)
            {
                return false;
            }
        }

        return true;
    }
}
