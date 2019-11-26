using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bait : EnemyData
{
    private static int SelfDamage = 4;

    public Bait()
        : base(name: "Bait", maxHP: 9, lives: 1, damage: 0, timer: 2, effect: $"Summon two Piranhas and lose {SelfDamage} hp.", spriteName: "Bait")
    { }

    override
    protected void AttackUniqueEffect()
    {
        EncounterManager.SpawnEnemyInDefaultManager(new Piranha());
        EncounterManager.SpawnEnemyInDefaultManager(new Piranha());
    }

    override public bool SelfHarm()
    {
        return DealDamage(SelfDamage);
    }
}
