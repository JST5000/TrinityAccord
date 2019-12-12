using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owl : EnemyData
{
    public Owl() : base(name: "Owl", maxHP: 8, lives: 3, damage: 4, timer: 3, 
        effect: InLineIcon.DAMAGE + ": 4, " + InLineIcon.ON_DISARM + ": Blind until the end of your next turn!", "Owl_Brown_On_White_BG")
    {
        wideSprite = true;
    }

    protected override void OnLossOfLife()
    {
        Player player = GameObject.Find("Player").GetComponent<Player>();
        player.Blind(2);
    }
}
