using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    [SerializeField]
    private Sprite _soundOn;

    [SerializeField]
    private Sprite _soundOff;

    [SerializeField]
    private Image _button;
    
    private bool _isSound;

    private void Start()
    {
        if (PlayerPrefs.GetInt("sound", 1) == 1)
        {
            _isSound = true;
            OnSound();
        }
        else
        {
            _isSound = false;
            OffSound();
        }
    }

    public void ClickButton()
    {
        if (_isSound)
        {
            OffSound();
            _isSound = false;
        }
        else
        {
            OnSound();
            _isSound = true;
        }
    }
    
    private void OnSound()
    {
        _button.sprite = _soundOn;
        Camera.main.GetComponent<AudioListener>().enabled = true;
    }

    private void OffSound()
    {
        _button.sprite = _soundOff;
        Camera.main.GetComponent<AudioListener>().enabled = false;
    }
    
}