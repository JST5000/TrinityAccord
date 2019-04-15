﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Executioner : EnemyData
{
    public Executioner() : base(name: "Executioner", maxHP: 14, staggers: 1, damage: 7, timer: 7, effect: InLineIcon.DAMAGE + ": 7, " + "Immune to debuffs", spriteName: "Executioner")
    { }

    public override void StaggerEnemy()
    {
        //Does nothing
    }

    public override void Stun()
    {
        //Does nothing
    }

}