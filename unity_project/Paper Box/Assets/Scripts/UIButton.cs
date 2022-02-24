using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    public void OnHover()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.sfxButtonHover);
    }

    public void OnClick()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.sfxButtonClick);
    }
}
