using System;
using System.Collections;
using System.Collections.Generic;
using Windows;
using UnityEngine;

public class CardsControllerWindow : BaseWindow
{

    [SerializeField] private List<CardAnimatorController> _animatorControllers;

    [SerializeField] private float _spinDuration;
    
    private IEnumerator StopRoll()
    {
        for (int i = 0; i < _animatorControllers.Count; i++)
        {
            yield return new WaitForSeconds(_spinDuration);
            _animatorControllers[i].StartStop();
            yield return new WaitForSeconds(_spinDuration);
        }
    }

    private void OnEnable()
    {
        StartCoroutine(StopRoll());
    }
}
