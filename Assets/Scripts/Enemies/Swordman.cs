using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swordman : EnemyData
{
    public Swordman() : base(name: "Sword", maxHP: 7, staggers: 2, damage: 2, timer: 2, effect: InLineIcon.DAMAGE + ": 2", "Swordman")
    { }

    public override EnemyData Copy()
    {
        return new Swordman();
    }
}
