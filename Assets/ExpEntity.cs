using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;

public class ExpEntity : MonoBehaviour
{

    [SerializeField] private MeshRenderer _mesh;
    [SerializeField] private int _needToRespawn;

    public bool isAlive;
    private int _turnCount;
    
    private void Awake()
    {
        NewEventSystem.OnTurnSwitch.Subscribe(OnTurnSwith);
    }

    private void OnDisable()
    {
        NewEventSystem.OnTurnSwitch.Subscribe(OnTurnSwith);
    }

    private void OnTurnSwith()
    {
        if(isAlive) return;
        _turnCount++;
        if (_turnCount >= _needToRespawn)
        {
            _turnCount = 0;
            _mesh.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Entity entity))
        {
            isAlive = false;
            _mesh.enabled = false;

            NewEventSystem.OnExpOrbPickup.InvokeEvent(entity.IsMine);
        }
    }
}
