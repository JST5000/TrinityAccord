using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Executioner : EnemyData
{
    public Executioner() : base(name: "Executioner", maxHP: 14, lives: 1, damage: 7, timer: 7, effect: InLineIcon.DAMAGE + ": 7, " + "Immune to debuffs", spriteName: "Executioner")
    {
        ImmuneToDebuffs = true;
    }
 
}
