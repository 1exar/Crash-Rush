using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    [SerializeField] private EntityContainer _container;
    [SerializeField] private TurnSwitcher _turnSwitcher;

    [SerializeField] private GameObject _enemyEntityPrefab;
    [SerializeField] private GameObject _playerEntityPrefab;
    
    [SerializeField] private EntityDataBase _entitys;

    public void PrepareSpawn(LevelSettings level)
    {
        foreach (var spawnPos in level.mySpawnPos)
        {
            SpawnEntity(true, spawnPos, _playerEntityPrefab, level.EntityOnLevel.Entitys[0].settings);
        }
        foreach (var spawnPos in level.enemySpawnPos)
        {
            SpawnEntity(false, spawnPos, _enemyEntityPrefab, level.EntityOnLevel.Entitys[1].settings);
        }
        
        _turnSwitcher.SwitchTurn();

    }
    
    public void SpawnEntity(List<Transform> _playerSpawnPositions, List<Transform> _enemySpawnPositions)
    {
        foreach (Transform spawnPos in _playerSpawnPositions)
        {
            Entity entity = Instantiate(_playerEntityPrefab, spawnPos.position, Quaternion.identity).GetComponent<Entity>();
            entity.Init(_entitys.Entitys[0].settings, true);
            _container.AddEntityToList(entity);
        }

        foreach (Transform spawnPos in _enemySpawnPositions)
        {
            Entity entity = Instantiate(_enemyEntityPrefab, spawnPos.position, Quaternion.identity).GetComponent<Entity>();
            entity.Init(_entitys.Entitys[1].settings, false);
            _container.AddEntityToList(entity);
        }

        _turnSwitcher.SwitchTurn();
    }

    public void SpawnEntity(bool isMine, Transform _transform,GameObject prefab, EntitySettings settings)
    {
        Entity _entity = Instantiate(prefab, _transform.position, Quaternion.identity).GetComponent<Entity>();
        _entity.Init(settings, isMine);
        
        _container.AddEntityToList(_entity);
    }
}
