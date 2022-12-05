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
    [SerializeField] private float _stopDuration;
    [SerializeField] private float _stopStep;
    [SerializeField] private Ease _animationEase;
    
    private Coroutine _animationCycle;

    public void StartStop()
    {
        float a = _cycleDuration;
        DOVirtual.Float(a, _stopStep, _stopDuration, (value => _cycleDuration = value)).SetEase(Ease.Linear)
            .OnComplete((() => StopCoroutine(_animationCycle)));
    }
    
    private void Start()
    {
        _animationCycle = StartCoroutine(RoolCardsCycle());
    }
    
    private IEnumerator RoolCardsCycle()
    {
        var _cardPos = _cards[0].transform.localPosition;
        _cards[2].transform.DOKill();
        _cards[2].transform.localPosition = new Vector3(_cardPos.x, _firstYpos * 2, _cardPos.z);
        _cards[0].transform.DOLocalMoveY(_firstYpos - (_step * 3), _cycleDuration).SetEase(_animationEase);
        _cards[1].transform.DOLocalMoveY(_firstYpos - (_step * 1), _cycleDuration).SetEase(_animationEase);;
        yield return new WaitForSeconds(_cycleDuration);
        _cards[1].transform.DOLocalMoveY(_firstYpos - (_step * 3), _cycleDuration).SetEase(_animationEase);;
        _cards[2].transform.DOLocalMoveY(_firstYpos - (_step * 1), _cycleDuration).SetEase(_animationEase);;
        _cards[0].transform.DOKill();
        _cards[0].transform.localPosition = new Vector3(_cardPos.x, _firstYpos * 2, _cardPos.z);
        yield return new WaitForSeconds(_cycleDuration);
        _cards[1].transform.DOKill();
        _cards[1].transform.localPosition = new Vector3(_cardPos.x, _firstYpos * 2, _cardPos.z);
        _cards[2].transform.DOLocalMoveY(_firstYpos - (_step * 3), _cycleDuration).SetEase(_animationEase);;
        _cards[0].transform.DOLocalMoveY(_firstYpos - (_step * 1), _cycleDuration).SetEase(_animationEase);; 
        yield return new WaitForSeconds(_cycleDuration);
        _animationCycle = StartCoroutine(RoolCardsCycle());
    }
}
