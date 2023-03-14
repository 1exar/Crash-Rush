using System;
using System.Collections;
using System.Collections.Generic;
using Windows;
using Events;
using UnityEngine;
using UnityEngine.Events;

public class EntitySpawner : MonoBehaviour
{
    [SerializeField] private EntityContainer _container;

    [SerializeField] private List<PosData> _playerSpawnPos;
    [SerializeField] private List<PosData> _enemySpawnPos;

    [SerializeField] private EntityDataBase _entityDataBase;

    private void Awake()
    {
        NewEventSystem.OnChooseNewBallEvent.Subscribe(SpawnNewBallFromCard);
        NewEventSystem.OnContainerRemoveEntity.Subscribe(OnEntityDead);
        NewEventSystem.OnPlayerLevelUp.Subscribe(OnEnemyLevelUp);
    }

    private void OnDestroy()
    {
        NewEventSystem.OnChooseNewBallEvent.UnSubscribe(SpawnNewBallFromCard);
        NewEventSystem.OnContainerRemoveEntity.UnSubscribe(OnEntityDead);
        NewEventSystem.OnPlayerLevelUp.UnSubscribe(OnEnemyLevelUp);
    }

    private void Start()
    {
        SpawnNewBallFromCard(EntityType.fire, false);
        SpawnNewBallFromCard(EntityType.ice, false);
        SpawnNewBallFromCard(EntityType.ice, true);
    }

    public bool isPlayerHasFreePos()
    {
        var isHas = false;

        foreach (var data in _playerSpawnPos)
        {
            if (data.isOccuped == false)
            {
                isHas = true;
                break;
            }
        }

        return isHas;
    }
    
    private void OnEnemyLevelUp(bool isPlayer)
    {
        if (isPlayer == false)
        {
            SpawnNewBallFromCard(EntityType.ice, false);
        }
    }
    
    private void OnEntityDead(Entity dead)
    {
        if (dead.IsMine)
        {
            foreach (var data in _playerSpawnPos)
            {
                var _posData = data;
                if (_posData.entity == dead)
                {
                    _posData.isOccuped = false;
                }
            }
        }
        else
        {
            foreach (var data in _enemySpawnPos)
            {
                var _posData = data;
                if (_posData.entity == dead)
                {
                    _posData.isOccuped = false;
                }
            }
        }
    }
    
    private void SpawnNewBallFromCard(EntityType type, bool isMine)
    {
        if (isMine)
        {
            for (int i = 0; i < _playerSpawnPos.Count; i++)
            {
                var _playerSpawnPo = _playerSpawnPos[i];
                if (_playerSpawnPo.isOccuped)
                    continue;

                var _entity = GetSpawnEntity(true, _playerSpawnPo.transform, _entityDataBase.GetPlayerEntityByType(type).prefab, _entityDataBase.GetPlayerEntityByType(type).settings);
                _playerSpawnPo.entity = _entity;
                _playerSpawnPo.isOccuped = true;
                break;
            }
        }
        else
        {
            for (int i = 0; i < _enemySpawnPos.Count; i++)
            {
                if(_enemySpawnPos[i].isOccuped)
                    continue;
                _enemySpawnPos[i].SetOccuped(true);
                //print(_enemySpawnPos[i].transform.position);
                var _entity = GetSpawnEntity(false, _enemySpawnPos[i].transform, _entityDataBase.GetEnemyEntityByType(type).prefab, _entityDataBase.GetEnemyEntityByType(type).settings);

                _enemySpawnPos[i].SetEntity(_entity);
                break;
            }
        }
    }

    private void SpawnEntity(bool isMine, Transform _transform,GameObject prefab, EntitySettings settings)
    {
        Entity _entity = Instantiate(prefab, _transform.position, Quaternion.identity).GetComponent<Entity>();
        _entity.Init(settings, isMine);
        
        _container.AddEntityToList(_entity);
    }
    
    private Entity GetSpawnEntity(bool isMine, Transform _transform,GameObject prefab, EntitySettings settings)
    {
        Entity _entity = Instantiate(prefab, _transform.position, Quaternion.identity).GetComponent<Entity>();
        _entity.Init(settings, isMine);
        
        _container.AddEntityToList(_entity);

        return _entity;
    }
    
}
[Serializable]
class PosData
{
    public Transform transform;
    public Entity entity;
    public bool isOccuped;

    public void SetEntity(Entity entity)
    {
        this.entity = entity;
    }

    public void SetOccuped(bool b)
    {
        isOccuped = b;
    }
}
