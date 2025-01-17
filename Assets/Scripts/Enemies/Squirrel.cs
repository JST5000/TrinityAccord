﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squirrel : EnemyData
{
    public Squirrel() : base(name: "Squirrel", maxHP: 4, lives: 3, damage: 2, timer: 2, effect: InLineIcon.DAMAGE + ": 2 " + InLineIcon.ON_DISARM + ": Draw an extra card next turn", spriteName: "Squirrel")
    { }

    override
    protected void OnLossOfLife()
    {
        DeckManager.Get().AddDrawNextTurn();
    }
}
