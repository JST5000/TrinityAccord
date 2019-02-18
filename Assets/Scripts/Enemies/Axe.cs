using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : EnemyData
{
    public Axe() : base(name: "Axe", maxHP: 7, staggers: 2, damage: 3, timer: 3, effect: "Deal 3 damage", "Axewoman")
    { }
}
