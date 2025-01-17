﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : EnemyData
{
    public Knight()
        : base(name: "Knight", maxHP: 3, lives: 2, damage: 2, timer: 3, effect: InLineIcon.DAMAGE + ": 2, Takes AT MOST 1 damage.", spriteName: "Knight")
    { }

    public override int GetModifiedDamageOnEachHit(int damage)
    {
        return Mathf.Min(1, damage);
    }

}
