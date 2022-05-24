using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSwitcher : MonoBehaviour
{
    [SerializeField] private EntityContainer _entityContainer;
    [SerializeField] private PlayerInputs _playerInputs;

    private Entity _currentEntity;
    private int _currentEntityNumber;

    public Entity CurrentEntity
    {
        get { return _currentEntity;  }
    }

    public void SwitchTurn()
    {
        foreach (var entity in _entityContainer.EnemyEntities)
        {
            entity._circleSprite.color = new Color(0, 0, 0, 0);
        }
        
        foreach (var entity in _entityContainer.PlayerEntities)
        {
            entity._circleSprite.color = new Color(0, 0, 0, 0);
        }
        
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
            _currentEntity._circleSprite.color = Color.white;
            _playerInputs.CanAim = true;
        }
        else
        {
            _currentEntity = _entityContainer.EnemyEntities[_currentEntityNumber];
            _currentEntity.GetComponent<EnemyEntityAim>().Aim();
            _currentEntity._circleSprite.color = Color.white;
        }
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
                SwitchTurn();
                yield break;
            }

            yield return new WaitForFixedUpdate();
        }
    }
}
