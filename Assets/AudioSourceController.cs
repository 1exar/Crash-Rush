using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;

public class AudioSourceController : MonoBehaviour
{

    private AudioSource _source;
    
    private void OnEnable()
    {
        _source = GetComponent<AudioSource>();
        NewEventSystem.onChangeSoundEvent.Subscribe(ChangeSoundState);
    }

    private void OnDisable()
    {
        NewEventSystem.onChangeSoundEvent.UnSubscribe(ChangeSoundState);
    }

    private void ChangeSoundState(bool state)
    {
        if(_source == null) return;
        if (state)
        {
            _source.volume = 1;
        }
        else
        {
            _source.volume = 0;
        }
    }
    
}
