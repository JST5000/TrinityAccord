using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : EnemyData
{

    private static int BaseDamage = 1;
    public Fly()
        : base(name: "Fly", maxHP: 1, lives: 1, damage: BaseDamage, timer: 1, effect: InLineIcon.DAMAGE + $": {BaseDamage}",
            spriteName: "Fly")
    { }
}
