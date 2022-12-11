using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    [SerializeField] private PreviewBallsAnimationController _cardAnimator;
    
    private Coroutine _animationCycle;

    [Header("TEST MODE")] 
    [SerializeField] private bool _testMode;
    [SerializeField] private EntityType _winType;
    
    public void StartStop()
    {
        float a = _cycleDuration;
        DOVirtual.Float(a, _stopStep, _stopDuration, (value => _cycleDuration = value)).SetEase(Ease.Linear)
            .OnComplete((() =>
            {
                StopCoroutine(_animationCycle);
                _cards.ForEach(card => card.canChoise = true);
            }));
    }
    
    private void OnEnable()
    {
        if (_testMode == false)
        {
            SetRandomCard(0, _cards[0]);
            Invoke(nameof(StartMove), .1f);
        }
        else
        {
            _cardAnimator.ShowBall(0, _winType);
            _cards[0].SetEntityType(_winType);
            _cards[0].canChoise = true;
        }

    }

    private void StartMove()
    {
        _animationCycle = StartCoroutine(RoolCardsCycle());
    }

    private IEnumerator RoolCardsCycle()
    {
        var _cardPos = _cards[0].transform.localPosition;
        SetRandomCard(1, _cards[1]);
        _cards[2].transform.DOKill();
        _cards[2].transform.localPosition = new Vector3(0, _firstYpos * 2, _cardPos.z);
        _cards[0].transform.DOLocalMoveY(_firstYpos - (_step * 3), _cycleDuration).SetEase(_animationEase);
        _cards[1].transform.DOLocalMoveY(_firstYpos - (_step * 1), _cycleDuration).SetEase(_animationEase);;
        yield return new WaitForSeconds(_cycleDuration);
        SetRandomCard(2,_cards[2]);
        _cards[1].transform.DOLocalMoveY(_firstYpos - (_step * 3), _cycleDuration).SetEase(_animationEase);;
        _cards[2].transform.DOLocalMoveY(_firstYpos - (_step * 1), _cycleDuration).SetEase(_animationEase);;
        _cards[0].transform.DOKill();
        _cards[0].transform.localPosition = new Vector3(0, _firstYpos * 2, _cardPos.z);
        yield return new WaitForSeconds(_cycleDuration);
        _cards[1].transform.DOKill();
        SetRandomCard(0,_cards[0]);
        _cards[1].transform.localPosition = new Vector3(0, _firstYpos * 2, _cardPos.z);
        _cards[2].transform.DOLocalMoveY(_firstYpos - (_step * 3), _cycleDuration).SetEase(_animationEase);;
        _cards[0].transform.DOLocalMoveY(_firstYpos - (_step * 1), _cycleDuration).SetEase(_animationEase);; 
        yield return new WaitForSeconds(_cycleDuration);
        _animationCycle = StartCoroutine(RoolCardsCycle());
    }

    private void SetRandomCard(int index, CardEntity card)
    {
        EntityType randCard = (EntityType) Random.Range(1, 4);
        _cardAnimator.ShowBall(index, randCard);
        card.SetEntityType(randCard);
    }
    
}
