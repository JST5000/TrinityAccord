using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : EnemyData
{
    public Axe() : base(name: "Axe", maxHP: 7, lives: 2, damage: 3, timer: 3, effect: InLineIcon.DAMAGE + ": 3", "Axewoman")
    { }

}
