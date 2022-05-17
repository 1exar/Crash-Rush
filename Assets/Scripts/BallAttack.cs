using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallAttack : MonoBehaviour
{
    [SerializeField]
    private int _damage;

    private bool _myAttack;

    private bool _isMine;

    private bool _myTeamTurn;
    [SerializeField]
    private Weapon[] _weapons;

    public int Damage
    {
        get {
            return _damage;
        }
        protected set {}
    }
    
    public void Init(bool isMine)
    {
        _isMine = isMine;
    }

    public void MyTurn()
    {
        _myAttack = true;
        foreach (var weapon in _weapons)
        {
            weapon.StartAimation();
        }
    }

    public void MyTeamAttack()
    {
        _myTeamTurn = true;
    }

    public void EnemyTeamAttack()
    {
        foreach (var weapon in _weapons)
        {
            weapon.StopAnimation();
        }
        _myTeamTurn = false;
        _myAttack = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_myAttack || _myTeamTurn)
        {
            if (collision.gameObject.TryGetComponent(out Ball _ball))
            {
                if (_ball.IsMine != _isMine)
                {
                    _ball.GetDamage(_damage);
                    foreach (var weapon in _weapons)
                    {
                     //   weapon.StartAimation();
                       // weapon.Fire();
                    }
                }
            }
        }
    }
}