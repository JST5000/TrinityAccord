using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GangWimp : EnemyData
{
    public GangWimp()
        : base(name: "Gang Wimp", maxHP: 4, lives: 1, damage: 1, timer: 2, effect: InLineIcon.DAMAGE + ": 1", spriteName: "GangWimp")
    { }
}
