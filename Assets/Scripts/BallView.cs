using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BallView: MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _circle;
    [SerializeField]
    private Color _myBall, _enemyBall, _select;
    [SerializeField]
    private BallMovment _movment;

    private bool _isMine;

    private void Awake()
    {
        _movment.OnMoved += CancleChoising;
    }

    public void SetCircleColor(bool _isMine)
    {

        this._isMine = _isMine;
        
        if (_isMine)
        {
            _circle.color = _myBall;
        }
        else
        {
            _circle.color = _enemyBall;
        }
    }

    public void ChoiseMe()
    {
        _circle.transform.DOScale(new Vector3(.3f, .3f, .3f), 1).SetLoops(-1, LoopType.Yoyo);
        _circle.DOColor(Color.yellow, 1);
    }
    
    private void CancleChoising(bool b)
    {
        _circle.transform.DOKill();
        _circle.transform.localScale = new Vector3(.4f, .4f, .4f);
        SetCircleColor(_isMine);
    }

    private void OnDisable()
    {
        _movment.OnMoved -= CancleChoising;
    }

    public void SelectBall()
    {
        _circle.color = _select;
    }

    public void DeselectBall()
    {
        SetCircleColor(_isMine);
    }
}
