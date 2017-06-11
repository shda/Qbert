using System.Collections;
using System.Collections.Generic;
using Assets.Qbert.Scripts;
using UnityEngine;

public class ButtonSoundOffOn : MonoBehaviour
{
    public Sprite spriteSoundOn;
    public Sprite spriteSoundOff;

    public SpriteRenderer imageSound;

    public void OnSoundSwitch()
    {
        GlobalValues.isSoundOn = !GlobalValues.isSoundOn;
        GlobalValues.Save();
        OnSwitchSound();
    }

    private void OnSwitchSound()
    {
        if (GlobalValues.isSoundOn)
        {
            imageSound.sprite = spriteSoundOn;
        }
        else
        {
            imageSound.sprite = spriteSoundOff;
        }
    }

    void Awake()
    {
        OnSwitchSound();
    }
}