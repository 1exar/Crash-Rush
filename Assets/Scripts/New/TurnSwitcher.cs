using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TurnSwitcher : MonoBehaviour
{
    [SerializeField] private EntityContainer _entityContainer;
    [SerializeField] private PlayerInputs _playerInputs;
    [SerializeField] private CurrentTurnLabel _currentTurnLabel;
    [SerializeField] private GameUI gameUI;
    [SerializeField] private ConfettiBlast confettiBlast;

    private Entity _currentEntity;
    private int _lastPlayerEntity;
    private int _lastEnemyEntity;
    
    public Entity CurrentEntity
    {
        get { return _currentEntity;  }
    }

    public void Awake()
    {
        EntitySpawner.OnChoiseNewBall += OnBallChosen;
    }

    public void OnBallChosen(EntityType a, bool b)
    {
        EntitySpawner.OnChoiseNewBall -= OnBallChosen;
        SwitchTurn();
    }
    
    public void SwitchTurn()
    {
        if (_entityContainer.EnemyEntities.Count == 0)
        {
            confettiBlast.Blast();
            gameUI.ShowWinPanel();
            return;
        }
        else if (_entityContainer.PlayerEntities.Count == 0)
        {
            gameUI.ShowLosePanel();
            print("no players");
            return;
        }

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
