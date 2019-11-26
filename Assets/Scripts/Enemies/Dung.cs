using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dung : EnemyData
{
    public Dung()
        : base(name: "Dung", maxHP: 3, lives: 1, damage: 0, timer: 1, effect: "Summon a Fly, " + InLineIcon.ON_DISARM + ": Summon a Fly", 
            spriteName: "Dung")
    { }

    override
    protected void AttackUniqueEffect()
    {
        EncounterManager.SpawnEnemyInDefaultManager(new Fly());
    }

    protected override void OnLossOfLife()
    {
        base.OnLossOfLife();
        EncounterManager.SpawnEnemyInDefaultManager(new Fly());
    }
}
