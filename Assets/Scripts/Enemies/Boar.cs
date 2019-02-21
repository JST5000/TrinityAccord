﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boar : EnemyData
{
    public Boar() : base(name: "Boar", maxHP: 6, staggers: 2, damage: 2, timer: 2, effect: GetBoarEffect(false), spriteName: "Boar")
    { }

    override
    protected void AttackUniqueEffect()
    {
    }

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
            return "Deal 4 damage.";
        }
        else
        {
            return "Deal 2 damage. Last Stand: Deal 2x Damage.";
        }
    }

}