using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rhino : EnemyData
{
    public Rhino() : base(name: "Rhino", maxHP: 8, staggers: 2, damage: 5, timer: 3, effect: "Deal 5 damage. Last Stand: Deal 2 damage instead of 5.")
    { }
}
