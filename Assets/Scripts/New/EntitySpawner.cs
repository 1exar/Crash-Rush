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

    public void SpawnEntity(List<Transform> _playerSpawnPositions, List<Transform> _enemySpawnPositions)
    {
        foreach (Transform spawnPos in _playerSpawnPositions)
        {
            Entity entity = Instantiate(_playerEntityPrefab, spawnPos.position, Quaternion.identity).GetComponent<Entity>();
            _container.AddEntityToList(entity);
            entity.Init(_entitys.Entitys[0].settings, true);
        }

        foreach (Transform spawnPos in _enemySpawnPositions)
        {
            Entity entity = Instantiate(_enemyEntityPrefab, spawnPos.position, Quaternion.identity).GetComponent<Entity>();
            _container.AddEntityToList(entity);
            entity.Init(_entitys.Entitys[0].settings, true);
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
