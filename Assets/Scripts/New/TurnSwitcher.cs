using System;
using System.Collections;
using System.Collections.Generic;
using Windows;
using UnityEngine;
using DG.Tweening;
using Events;
using UnityEngine.Events;

public class TurnSwitcher : MonoBehaviour
{
    [SerializeField] private EntityContainer _entityContainer;
    [SerializeField] private PlayerInputs _playerInputs;
    [SerializeField] private CurrentTurnLabel _currentTurnLabel;
    [SerializeField] private ConfettiBlast confettiBlast;

    private Entity _currentEntity;
    private int _lastPlayerEntity;
    private int _lastEnemyEntity;

    private bool _canTurn = true;
    
    public Entity CurrentEntity
    {
        get { return _currentEntity;  }
    }

    private void Awake()
    {
        NewEventSystem.OnChooseNewBallEvent.Subscribe(OnBallChosen);
        NewEventSystem.OnContainerRemoveEntity.Subscribe(CheckOnEntityDead);
        NewEventSystem.OnPlayerLevelUp.Subscribe(OnPlayerLevelUp);
    }

    private void OnDisable()
    {
        NewEventSystem.OnContainerRemoveEntity.UnSubscribe(CheckOnEntityDead);
        NewEventSystem.OnChooseNewBallEvent.UnSubscribe(OnBallChosen);
        NewEventSystem.OnPlayerLevelUp.UnSubscribe(OnPlayerLevelUp);
    }

    private void OnPlayerLevelUp(bool isPlayer)
    {
        if(isPlayer)
            _canTurn = false;
    }
    
    private void OnBallChosen(EntityType a, bool b)
    {
        _canTurn = true;
        SwitchTurn();
    }

    private void CheckOnEntityDead(Entity entity)
    {
        if (entity == _currentEntity || _currentEntity == null)
        {
            SwitchTurn();
        }
    }
    
    public void SwitchTurn()
    {
        if(_canTurn == false) return;
        
        if (_entityContainer.EnemyEntities.Count == 0)
        {
            confettiBlast.Blast();
            WindowController.ShowWindow(typeof(WinWindow));
            return;
        }
        else if (_entityContainer.PlayerEntities.Count == 0)
        {
            WindowController.ShowWindow(typeof(FailWindow));
            print("no players");
            return;
        }

        NewEventSystem.OnTurnSwitch.InvokeEvent();

        foreach (var entity in _entityContainer.EnemyEntities)
        {
            entity.CircleSprite.color = Color.clear;
        }
        
        foreach (var entity in _entityContainer.PlayerEntities)
        {
            entity.CircleSprite.color = Color.clear;
        }

        bool isPlayerNextTurn = true;
        if (_currentEntity != null)
        {
            if (_currentEntity.IsMine)
            {
                isPlayerNextTurn = false;
                _lastEnemyEntity += 1;
                if (_lastEnemyEntity >= _entityContainer.EnemyEntities.Count) _lastEnemyEntity = 0;
            }
            else
            {
                _lastPlayerEntity += 1;
                if (_lastPlayerEntity >= _entityContainer.PlayerEntities.Count) _lastPlayerEntity = 0;
            }
        }

        if (isPlayerNextTurn)
        {
            _currentTurnLabel.SetText("YOUR TURN");
            _currentEntity = _entityContainer.PlayerEntities[_lastPlayerEntity];
            _currentEntity.CircleSprite.color = Color.green;
            _playerInputs.CanAim = true;
        }
        else
        {
            _currentTurnLabel.SetText("ENEMY'S TURN");
            _currentEntity = _entityContainer.EnemyEntities[_lastEnemyEntity];
            _currentEntity.GetComponent<EnemyEntityAiming>().Aim();
            _currentEntity.CircleSprite.color = Color.red;
        }

        _currentTurnLabel.Show();
    }

    public void PrepareToSwitch()
    {
        StartCoroutine(CheckIfReadyToSwitch());
    }

    private IEnumerator CheckIfReadyToSwitch()
    {
        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            bool ready = true;
            foreach (Entity entity in _entityContainer.PlayerEntities)
            {
                if (entity.GetComponent<Rigidbody>().velocity != Vector3.zero)
                {
                    ready = false;
                    break;
                }
            }

            foreach (Entity entity in _entityContainer.EnemyEntities)
            {
                if (entity.GetComponent<Rigidbody>().velocity != Vector3.zero)
                {
                    ready = false;
                    break;
                }
            }

            if (ready)
            {
                foreach (Entity entity in _entityContainer.EnemyEntities) entity.transform.eulerAngles = Vector3.up * 180;
                foreach (Entity entity in _entityContainer.PlayerEntities) entity.transform.eulerAngles = Vector3.up * 180;
                SwitchTurn();
                yield break;
            }

            yield return new WaitForFixedUpdate();
        }
    }
}
