using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityContainer : MonoBehaviour
{
    [SerializeField] private List<Entity> _playerEntities = new List<Entity>();
    [SerializeField] private List<Entity> _enemyEntities = new List<Entity>();

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
}
