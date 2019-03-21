using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : EnemyData
{
    public Knight()
        : base(name: "Knight", maxHP: 3, staggers: 2, damage: 2, timer: 3, effect: InLineIcon.DAMAGE + ": 2, Takes up to 1 damage per hit.", spriteName: "Knight")
    { }

    public override int GetModifiedDamageOnEachHit(int damage)
    {
        return Mathf.Min(1, damage);
    }

    public override EnemyData Copy()
    {
        return new Knight();
    }
}
