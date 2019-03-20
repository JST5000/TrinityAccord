using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doombringer : EnemyData
{
    public Doombringer() 
        : base(name: "Doombringer", maxHP: 8, staggers: 2, damage: 0, timer: 3, effect: "Summon a Spirit", spriteName: GlobalVars.DEMO_ART ? "FantasyCharacters_lich" : "Doombringer")
    { }

    override
    protected void AttackUniqueEffect()
    {
        EncounterManager.SpawnEnemyInDefaultManager(new Spirit());
    }

    public override EnemyData Copy()
    {
        return new Doombringer();
    }
}
