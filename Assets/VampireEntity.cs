using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireEntity : Entity
{
    public override void AttackEntity(Entity _enemy, float dmg)
    {
        base.AttackEntity(_enemy, dmg);
        ApplyHealEffect((int)dmg);
    }
}
