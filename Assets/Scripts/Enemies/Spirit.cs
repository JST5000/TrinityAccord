using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spirit : EnemyData
{
    public Spirit() : base(name: "Spirit", maxHP: 6, lives: 1, damage: 3, timer: 1, effect: InLineIcon.DAMAGE + ": 3", spriteName: GlobalVars.DEMO_ART ? "FantasyCharacters_ghost" : "spirit")
    { }

}
