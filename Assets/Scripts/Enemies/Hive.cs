using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hive : EnemyData
{
    public Hive()
        : base(name: "Hive", maxHP: 9, staggers: 1, damage: 0, timer: 2, effect: "Summon a Wasp and lose 3 hp.", spriteName: "Hive")
    { }

    override
    protected void AttackUniqueEffect()
    {
        EncounterManager.SpawnEnemyInDefaultManager(new Wasp());
    }

    override public bool SelfHarm()
    {
        return DealDamage(3);
    }

    public override EnemyData Copy()
    {
        return new Hive();
    }
}
