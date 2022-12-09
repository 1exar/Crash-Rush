using System;
using System.Collections;
using System.Collections.Generic;
using Windows;
using UnityEngine;
using UnityEngine.Events;

public class EntitySpawner : MonoBehaviour
{
    [SerializeField] private EntityContainer _container;

    [SerializeField] private List<Transform> _playerSpawnPos;
    [SerializeField] private List<Transform> _enemySpawnPos;

    [SerializeField] private EntityDataBase _entityDataBase;

    private Dictionary<Transform, bool> _isPosChoisePlayer = new Dictionary<Transform, bool>();
    private Dictionary<Transform, bool> _isPosChoiseEnemy = new Dictionary<Transform, bool>();

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
        WindowController.CloseWindow(typeof(CardsControllerWindow));
    }

    private void Start()
    {
        SpawnNewBallFromCard(EntityType.fire, false);
        SpawnNewBallFromCard(EntityType.ice, false);
    }

    private void SpawnNewBallFromCard(EntityType type, bool isMine)
    {
        if (isMine)
        {
            for (int i = 0; i < _playerSpawnPos.Count; i++)
            {
                if(_isPosChoisePlayer.ContainsKey(_playerSpawnPos[i]))
                    continue;
                
                SpawnEntity(true, _playerSpawnPos[i], _entityDataBase.GetEntityByType(type).prefab, _entityDataBase.GetEntityByType(type).settings);
                _isPosChoisePlayer.Add(_playerSpawnPos[i], true);
                break;
            }
        }
        else
        {
            for (int i = 0; i < _enemySpawnPos.Count; i++)
            {
                if(_isPosChoiseEnemy.ContainsKey(_enemySpawnPos[i]))
                    continue;
                
                SpawnEntity(false, _enemySpawnPos[i], _entityDataBase.GetEntityByType(type).prefab, _entityDataBase.GetEntityByType(type).settings);
                _isPosChoiseEnemy.Add(_enemySpawnPos[i], true);
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

}
