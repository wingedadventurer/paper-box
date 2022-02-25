using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    [SerializeField] private MeshRenderer[] mrDigits;
    [SerializeField] private int[] remap;
    [SerializeField] private Material matOn;
    [SerializeField] private Material matOff;

    int h = 1;
    int m = 1;
    int s = 1;

    void Start()
    {
        SetTime(0);
    }

    public void SetTime(float t)
    {
        int seconds = (int)(t);
        int hours = seconds / 3600;
        seconds %= 3600;
        int minutes = seconds / 60;
        seconds %= 60;
        
        if (h != hours)
        {
            h = hours;
            SetDigit(0, h / 10);
            SetDigit(1, h % 10);
        }
        if (m != minutes)
        {
            m = minutes;
            SetDigit(2, m / 10);
            SetDigit(3, m % 10);
        }
        if (s != seconds)
        {
            s = seconds;
            SetDigit(4, s / 10);
            SetDigit(5, s % 10);
        }
    }

    private void SetDigit(int index, int number)
    {
        MeshRenderer digit = mrDigits[index];
        Material[] mats = digit.sharedMaterials;

        switch (number)
        {
            case 0:
                mats[remap[0]] = matOn;
                mats[remap[1]] = matOn;
                mats[remap[2]] = matOn;
                mats[remap[3]] = matOff;
                mats[remap[4]] = matOn;
                mats[remap[5]] = matOn;
                mats[remap[6]] = matOn;
                break;
            case 1:
                mats[remap[0]] = matOff;
                mats[remap[1]] = matOff;
                mats[remap[2]] = matOn;
                mats[remap[3]] = matOff;
                mats[remap[4]] = matOff;
                mats[remap[5]] = matOn;
                mats[remap[6]] = matOff;
                break;
            case 2:
                mats[remap[0]] = matOn;
                mats[remap[1]] = matOff;
                mats[remap[2]] = matOn;
                mats[remap[3]] = matOn;
                mats[remap[4]] = matOn;
                mats[remap[5]] = matOff;
                mats[remap[6]] = matOn;
                break;
            case 3:
                mats[remap[0]] = matOn;
                mats[remap[1]] = matOff;
                mats[remap[2]] = matOn;
                mats[remap[3]] = matOn;
                mats[remap[4]] = matOff;
                mats[remap[5]] = matOn;
                mats[remap[6]] = matOn;
                break;
            case 4:
                mats[remap[0]] = matOff;
                mats[remap[1]] = matOn;
                mats[remap[2]] = matOn;
                mats[remap[3]] = matOn;
                mats[remap[4]] = matOff;
                mats[remap[5]] = matOn;
                mats[remap[6]] = matOff;
                break;
            case 5:
                mats[remap[0]] = matOn;
                mats[remap[1]] = matOn;
                mats[remap[2]] = matOff;
                mats[remap[3]] = matOn;
                mats[remap[4]] = matOff;
                mats[remap[5]] = matOn;
                mats[remap[6]] = matOn;
                break;
            case 6:
                mats[remap[0]] = matOn;
                mats[remap[1]] = matOn;
                mats[remap[2]] = matOff;
                mats[remap[3]] = matOn;
                mats[remap[4]] = matOn;
                mats[remap[5]] = matOn;
                mats[remap[6]] = matOn;
                break;
            case 7:
                mats[remap[0]] = matOn;
                mats[remap[1]] = matOff;
                mats[remap[2]] = matOn;
                mats[remap[3]] = matOff;
                mats[remap[4]] = matOff;
                mats[remap[5]] = matOn;
                mats[remap[6]] = matOff;
                break;
            case 8:
                mats[remap[0]] = matOn;
                mats[remap[1]] = matOn;
                mats[remap[2]] = matOn;
                mats[remap[3]] = matOn;
                mats[remap[4]] = matOn;
                mats[remap[5]] = matOn;
                mats[remap[6]] = matOn;
                break;
            case 9:
                mats[remap[0]] = matOn;
                mats[remap[1]] = matOn;
                mats[remap[2]] = matOn;
                mats[remap[3]] = matOn;
                mats[remap[4]] = matOff;
                mats[remap[5]] = matOn;
                mats[remap[6]] = matOn;
                break;
        }
        digit.sharedMaterials = mats;
    }
}
