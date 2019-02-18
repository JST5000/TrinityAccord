using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swordman : EnemyData
{
    public Swordman() : base(name: "Sword", maxHP: 7, staggers: 2, damage: 2, timer: 2, effect: "Deal 2 damage", "Swordman")
    { }
}
