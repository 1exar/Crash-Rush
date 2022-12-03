using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardAnimatorController : MonoBehaviour
{

    [SerializeField] private List<CardEntity> _cards;

    [SerializeField] private int _step = 450;
    [SerializeField] private int _firstYpos = 225;

    [SerializeField] private float _cycleDuration;

    [SerializeField] private bool _startStop;
    [SerializeField] private float _stopSpeed;
    [SerializeField] private float _stopEndValue;

    private Coroutine _animationCycle;

    public void StartStop()
    {
        _startStop = true;
    }
    
    private void Start()
    {
        _stopEndValue = Random.Range(1f, 10f);
        _animationCycle = StartCoroutine(RoolCardsCycle());
    }

    private IEnumerator RoolCardsCycle()
    {
        var _cardPos = _cards[0].transform.localPosition;
        yield return new WaitForFixedUpdate();
        _cards[2].transform.DOKill();
        _cards[2].transform.localPosition = new Vector3(_cardPos.x, _firstYpos * 2, _cardPos.z);
        _cards[0].transform.DOLocalMoveY(_firstYpos - (_step * 3), _cycleDuration);
        _cards[1].transform.DOLocalMoveY(_firstYpos - (_step * 1), _cycleDuration);
        yield return new WaitForSeconds(_cycleDuration);
        _cards[1].transform.DOLocalMoveY(_firstYpos - (_step * 3), _cycleDuration);
        _cards[2].transform.DOLocalMoveY(_firstYpos - (_step * 1), _cycleDuration);
        yield return new WaitForFixedUpdate();
        _cards[0].transform.DOKill();
        _cards[0].transform.localPosition = new Vector3(_cardPos.x, _firstYpos * 2, _cardPos.z);
        yield return new WaitForSeconds(_cycleDuration);
        _cards[1].transform.DOKill();
        _cards[1].transform.localPosition = new Vector3(_cardPos.x, _firstYpos * 2, _cardPos.z);
        yield return new WaitForFixedUpdate();
        _cards[2].transform.DOLocalMoveY(_firstYpos - (_step * 3), _cycleDuration);
        _cards[0].transform.DOLocalMoveY(_firstYpos - (_step * 1), _cycleDuration);
        yield return new WaitForSeconds(_cycleDuration);
        _animationCycle = StartCoroutine(RoolCardsCycle());
    }

    private void FixedUpdate()
    {
        if (_startStop)
        {
            _cycleDuration += _stopSpeed * Time.fixedTime;
            if (_cycleDuration >= _stopEndValue)
            {
                _startStop = false;
                StopCoroutine(_animationCycle);
            }
        }
    }
}
