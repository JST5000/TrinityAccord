using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bull : EnemyData
{
    private static int BaseDamage = 3;

    public Bull()
        : base(name: "Bull", maxHP: 10, lives: 2, damage: BaseDamage, timer: 3, effect: InLineIcon.DAMAGE + $": {BaseDamage}, Summon Dung.", spriteName: "Bull")
    { }

    override
    protected void AttackUniqueEffect()
    {
        EncounterManager.SpawnEnemyInDefaultManager(new Dung());
    }
}

