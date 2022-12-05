using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EntitySpawner : MonoBehaviour
{
    [SerializeField] private EntityContainer _container;
    [SerializeField] private TurnSwitcher _turnSwitcher;

    [SerializeField] private List<Transform> _playerSpawnPos;

    [SerializeField] private EntityDataBase _entityDataBase;

    private Dictionary<Transform, bool> _isPosChoise = new Dictionary<Transform, bool>();

    public static UnityAction<EntityType, bool> OnChoiseNewBall;

    private void Awake()
    {
        OnChoiseNewBall += SpawnNewBallFromCard;
    }

    private void OnDestroy()
    {
        OnChoiseNewBall -= SpawnNewBallFromCard;
    }

    public static void InvokeChoiseBallEvent(EntityType type, bool isMine)
    {
        OnChoiseNewBall?.Invoke(type, isMine);
    }

    private void SpawnNewBallFromCard(EntityType type, bool isMine)
    {
        if (isMine)
        {
            for (int i = 0; i < _playerSpawnPos.Count; i++)
            {
                if(_isPosChoise.ContainsKey(_playerSpawnPos[i]))
                    continue;
                
                SpawnEntity(true, _playerSpawnPos[i], _entityDataBase.GetEntityByType(type, true).prefab, _entityDataBase.GetEntityByType(type, true).settings);
                _isPosChoise.Add(_playerSpawnPos[i], true);
                break;
            }
            _turnSwitcher.SwitchTurn();
        }
    }

    private void SpawnEntity(bool isMine, Transform _transform,GameObject prefab, EntitySettings settings)
    {
        Entity _entity = Instantiate(prefab, _transform.position, Quaternion.identity).GetComponent<Entity>();
        _entity.Init(settings, isMine);
        
        _container.AddEntityToList(_entity);
    }

}
