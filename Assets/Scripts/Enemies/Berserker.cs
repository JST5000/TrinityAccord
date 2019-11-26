using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berserker : EnemyData
{
    public Berserker()
        : base(name: "Berserker", maxHP: 4, lives: 2, damage: 3, timer: 3, effect: InLineIcon.DAMAGE + ": 3, Takes 1 less damage per hit.", spriteName: "Berserker")
    { }

    public override int GetModifiedDamageOnEachHit(int damage)
    {
        return Mathf.Max(0, damage - 1);
    }
}
