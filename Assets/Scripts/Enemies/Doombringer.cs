using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doombringer : EnemyData
{
    public Doombringer() : base(name: "Doombringer", maxHP: 8, staggers: 2, damage: 0, timer: 3, effect: "Summon a Spirit", spriteName: "Doombringer")
    { }

    override
    protected void AttackUniqueEffect()
    {
        EncounterManager.SpawnEnemyInDefaultManager(new Spirit());
    }

}
