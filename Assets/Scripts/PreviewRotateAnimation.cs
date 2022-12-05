using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PreviewRotateAnimation : MonoBehaviour
{

    [SerializeField] private float _speed;
    
    void Start()
    {
        transform.DORotate(transform.localRotation.eulerAngles + Vector3.right + Vector3.forward, _speed, RotateMode.Fast).SetEase(Ease.Linear).OnComplete(Start);
    }

}
