using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    [SerializeField] private EntityContainer _container;
    [SerializeField] private TurnSwitcher _turnSwitcher;

    [SerializeField] private GameObject _enemyEntityPrefab;
    [SerializeField] private GameObject _playerEntityPrefab;
    [SerializeField] private Transform[] _enemySpawnPositions;
    [SerializeField] private Transform[] _playerSpawnPositions;

    private void Start()
    {
        foreach (Transform spawnPos in _playerSpawnPositions)
        {
            Entity entity = Instantiate(_playerEntityPrefab, spawnPos.position, Quaternion.identity).GetComponent<Entity>();
            _container.AddEntityToList(entity);
        }

        foreach (Transform spawnPos in _enemySpawnPositions)
        {
            Entity entity = Instantiate(_enemyEntityPrefab, spawnPos.position, Quaternion.identity).GetComponent<Entity>();
            _container.AddEntityToList(entity);
        }

        _turnSwitcher.SwitchTurn();
    }
}
