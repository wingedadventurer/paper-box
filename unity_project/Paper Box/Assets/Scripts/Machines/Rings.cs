using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rings : MonoBehaviour
{
    private const int RING_COUNT = 3;
    private const int ROTATIONS_COUNT = 8;

    [SerializeField] private Transform[] trRings;
    [SerializeField] private GameObject goSolidBefore;
    [SerializeField] private GameObject goSolidAfter;
    [SerializeField] private GameObject goKey;
    [SerializeField] private GameButton[] buttons;
    private int[] rotations = new int[RING_COUNT];
    private float[] angles = new float[RING_COUNT];

    private float rotationAngle;

    private void Start()
    {
        rotationAngle = 360f / ROTATIONS_COUNT;
        rotations[0] = 5;
        rotations[1] = 2;
        rotations[2] = 3;
        goSolidBefore.SetActive(true);
        goSolidAfter.SetActive(false);
        goKey.SetActive(false);
    }

    private void Update()
    {
        // debug control
        //if (Input.GetKeyDown(KeyCode.Keypad1)) { OnButtonPress(0); }
        //if (Input.GetKeyDown(KeyCode.Keypad2)) { OnButtonPress(1); }
        //if (Input.GetKeyDown(KeyCode.Keypad3)) { OnButtonPress(2); }

        for (int i = 0; i < RING_COUNT; i++)
        {
            angles[i] = Mathf.LerpAngle(angles[i], rotations[i] * rotationAngle, 0.075f);
            trRings[i].localEulerAngles = new Vector3(-90, 0, angles[i]);
        }
    }

    public void OnButtonPress(int index)
    {
        switch (index)
        {
            case 0:
                Rotate(0);
                Rotate(0);
                Rotate(1, true);
                break;
            case 1:
                Rotate(1);
                Rotate(1);
                Rotate(2, true);
                break;
            case 2:
                Rotate(2);
                Rotate(2);
                Rotate(0, true);
                break;
            default:
                break;
        }
    }

    private void Rotate(int index, bool reverse = false)
    {
        if (reverse)
        {
            rotations[index] -= 1;
            if (rotations[index] < 0)
            {
                rotations[index] = ROTATIONS_COUNT - 1;
            }
        }
        else
        {
            rotations[index] += 1;
            if (rotations[index] == ROTATIONS_COUNT)
            {
                rotations[index] = 0;
            }
        }

        if (IsCompleted())
        {
            goSolidBefore.SetActive(false);
            goSolidAfter.SetActive(true);
            goKey.SetActive(true);

            foreach (GameButton button in buttons)
            {
                button.SetInteractable(false);
                button.toggle = true;
                button.SetPressed(true, true, true);
            }
        }
    }

    private bool IsCompleted()
    {
        for (int i = 0; i < RING_COUNT; i++)
        {
            if (rotations[i] != 0) { return false; }
        }

        return true;
    }
}
