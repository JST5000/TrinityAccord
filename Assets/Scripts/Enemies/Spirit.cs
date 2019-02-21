using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spirit : EnemyData
{
    public Spirit() : base(name: "Spirit", maxHP: 6, staggers: 1, damage: 3, timer: 1, effect: "Deal 3 damage", spriteName: GlobalVars.DEMO_ART ? "FantasyCharacters_ghost" : "spirit")
    { }
}
