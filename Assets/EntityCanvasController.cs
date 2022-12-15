using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EntityCanvasController : MonoBehaviour
{

    [SerializeField] private Slider _health;
    [Header("Damage Text Animation")]
    [SerializeField] private TMP_Text _damageIndicator;
    [SerializeField] private float _localYUp;
    [SerializeField] private float _daration;
    private float _startY;

    private void Awake()
    {
        _startY = _damageIndicator.rectTransform.localPosition.y;
    }

    public void SetHealthMax(float max)
    {
        _health.maxValue = max;
        SetHealth(max);
    }
    
    public void ShowDamage(float dmg)
    {
        _damageIndicator.rectTransform.DOKill();
        _damageIndicator.text = "-" + (int)dmg;
        _damageIndicator.gameObject.SetActive(true);
        _damageIndicator.rectTransform.DOLocalMoveY(_startY + _localYUp, _daration).OnComplete(() => {
            _damageIndicator.rectTransform.DOLocalMoveY(_startY, 0);
            _damageIndicator.gameObject.SetActive(false);
        });
    }

    public void SetHealth(float hp)
    {
        _health.value = hp;
    }
    
}
