using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _text;
    
    public void Init(int _damage)
    {
        _text.text = "-" + _damage;
    }

    private IEnumerator Start()
    {
        transform.DOLocalMove(Vector3.down * 100, 2f);
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
