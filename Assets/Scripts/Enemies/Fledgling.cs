using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fledgling : EnemyData
{
    private static int damage = 2;

    public Fledgling() : base(name: "Fledgling", maxHP: 6, lives: 1, damage: damage, timer: 2, effect: InLineIcon.DAMAGE + ": " + damage, "Fledgling_Colorized")
    { }
}
