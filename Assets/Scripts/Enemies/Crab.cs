using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crab : EnemyData
{
    private static int BaseDamage = 3;

    public Crab() : base(name: "Crab", maxHP: 5, lives: 2, damage: BaseDamage, timer: 2, effect: InLineIcon.DAMAGE + $": {BaseDamage}", "Crab")
    { }
}
