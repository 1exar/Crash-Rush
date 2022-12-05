using UnityEngine;

public class FireEntity : Entity
{
    [SerializeField] private int _fireDmg;
    [SerializeField] private int _fireDuration;
    
    public override void AttackEntity(Entity _enemy, float dmg)
    {
        base.AttackEntity(_enemy, dmg);
        _enemy.ApplyFireEffect(_fireDmg, _fireDuration);
    }

}
