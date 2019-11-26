using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : EnemyData
{
    public Shark()
        : base(name: "Shark", maxHP: 6, lives: 3, damage: 4, timer: 2, effect: InLineIcon.DAMAGE + ": 4", spriteName: "Shark")
    { }
}
