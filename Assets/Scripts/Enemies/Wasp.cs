﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wasp : EnemyData
{
    public Wasp()
        : base(name: "Wasp", maxHP: 5, lives: 1, damage: 2, timer: 1, effect: InLineIcon.DAMAGE + ": 2", spriteName: "Wasp")
    { }

}
