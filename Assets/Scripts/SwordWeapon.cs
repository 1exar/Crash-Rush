using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SwordWeapon : Weapon
{
    [SerializeField]
    private Transform _rotateParent;
    [SerializeField]
    private Vector3 _axis;
    [SerializeField]
    private float _angel;
    private bool _rotate;
    [SerializeField]
    private TrailRenderer _trail;

    private float _positionY;

    private void Awake()
    {
        _positionY = transform.localPosition.y;
    }

    public override void Fire()
    {
    }

    public override void StartAimation()
    {
        _rotate = true;
        _trail.enabled = true;
    }

    public override void StopAnimation()
    {
        _rotate = false;
        _trail.enabled = false;
    }

    private void FixedUpdate()
    {
        if (_rotate == false) return;
        
        transform.RotateAround(_rotateParent.position, _axis, _angel);
        transform.localPosition = new Vector3(transform.localPosition.x, _positionY, transform.localPosition.z);
    }

    private IEnumerator ProccesAnimation()
    {
        _rotate = true;
        yield return new WaitForSeconds(1f);
        _rotate = false;
    }
    
}
