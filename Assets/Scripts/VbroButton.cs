using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VbroButton : MonoBehaviour
{
    [SerializeField]
    private Sprite _vibroOn, _vibroOff;
    [SerializeField]
    private Image _button;
    
    private bool _isVibro;

    private void Start()
    {
        if (PlayerPrefs.GetInt("vibro", 1) == 1)
        {
            _isVibro = true;
            OnVibro();
        }
        else
        {
            _isVibro = false;
            OffVibro();
        }
    }

    public void ClickButton()
    {
        if (_isVibro)
        {
            OffVibro();
            _isVibro = false;
        }
        else
        {
            OnVibro();
            _isVibro = true;
        }
    }
    
    private void OnVibro()
    {
        _button.sprite = _vibroOn;
    }

    private void OffVibro()
    {
        _button.sprite = _vibroOff;
    }
    
}
