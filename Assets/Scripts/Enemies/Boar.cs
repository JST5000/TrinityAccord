﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boar : EnemyData
{
    public Boar() : base(name: "Boar", maxHP: 6, staggers: 2, damage: 2, timer: 2, effect: GetBoarEffect(false), spriteName: "Boar")
    { }

    protected override void OnLastLife()
    {
        //Double damage
        Damage = 2 * Damage;
        Effect = GetBoarEffect(true);
    }

    private static string GetBoarEffect(bool lastLife)
    {
        if (lastLife)
        {
            return InLineIcon.DAMAGE + ": 4";
        }
        else
        {
            return InLineIcon.DAMAGE + ": 2 Last Stand: " + InLineIcon.DAMAGE + ": 4";
        }
    }

    public override EnemyData Copy()
    {
        return new Boar();
    }

}
