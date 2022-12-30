using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderEntity : Entity
{
    [SerializeField]
    private int _thunderDmg;
    
    public override void AttackEntity(Entity _enemy, float dmg)
    {
        base.AttackEntity(_enemy, dmg);
        print("thunder attack");
        AttackAllEnemy();
    }

    private void AttackAllEnemy()
    {
        if (IsMine)
        {
            _container.EnemyEntities.ForEach(entity => entity.TakeDamage(_thunderDmg));
        }
        else
        {
            _container.PlayerEntities.ForEach(entity => entity.TakeDamage(_thunderDmg));
        }
    }
    
}
