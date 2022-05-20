using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSwitcher : MonoBehaviour
{
    [SerializeField] private EntityContainer _entityContainer;
    [SerializeField] private PlayerInputs _playerInputs;

    private Entity _currentEntity = null;
    private int _currentEntityNumber = 0;

    public Entity CurrentEntity
    {
        get { return _currentEntity;  }
    }

    public void SwitchTurn()
    {
        bool isPlayerNextTurn = true;

        if (_currentEntity != null)
        {
            if (_currentEntity.IsMine)
            {
                isPlayerNextTurn = false;
                if (_currentEntityNumber >= _entityContainer.EnemyEntities.Count) _currentEntityNumber = 0;
            }
            else
            {
                _currentEntityNumber += 1;
                if (_currentEntityNumber >= _entityContainer.PlayerEntities.Count) _currentEntityNumber = 0;
            }
        }

        if (isPlayerNextTurn)
        {
            _currentEntity = _entityContainer.PlayerEntities[_currentEntityNumber];
            _playerInputs.CanAim = true;
        }
        else
        {
            _currentEntity = _entityContainer.EnemyEntities[_currentEntityNumber];
            _currentEntity.GetComponent<EnemyEntityAim>().Aim();
        }
    }

    public void PrepareToSwitch()
    {
        StartCoroutine(CheckIfReadyToSwitch());
    }

    private IEnumerator CheckIfReadyToSwitch()
    {
        while (true)
        {
            bool ready = true;
            foreach (Entity entity in _entityContainer.PlayerEntities)
            {
                if (entity.GetComponent<EntityMovement>().CurrentSpeed != 0)
                {
                    ready = false;
                    break;
                }
            }
            foreach (Entity entity in _entityContainer.EnemyEntities)
            {
                if (entity.GetComponent<EntityMovement>().CurrentSpeed != 0)
                {
                    ready = false;
                    break;
                }
            }

            if (ready)
            {
                SwitchTurn();
                yield break;
            }

            yield return new WaitForFixedUpdate();
        }
    }
}
