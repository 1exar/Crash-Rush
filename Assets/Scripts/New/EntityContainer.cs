using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EntityContainer : MonoBehaviour
{
    [SerializeField] private List<Entity> _playerEntities = new List<Entity>();
    [SerializeField] private List<Entity> _enemyEntities = new List<Entity>();

    public static UnityAction<Entity> OnRemoveEntity;

    public List<Entity> PlayerEntities
    {
        get { return _playerEntities; }
    }

    public List<Entity> EnemyEntities
    {
        get { return _enemyEntities; }
    }

    public void AddEntityToList(Entity newEntity)
    {
        if (newEntity.IsMine) _playerEntities.Add(newEntity);
        else _enemyEntities.Add(newEntity);
    }

    public void RemoveEntityFromList(Entity entity, bool isMine)
    {
        if (isMine)
        {
            _playerEntities.Remove(entity);
        }
        else
        {
            _enemyEntities.Remove(entity);
        }

        OnRemoveEntity?.Invoke(entity);
    }
}
