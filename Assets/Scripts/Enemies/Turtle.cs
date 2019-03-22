using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : EnemyData
{
    public Turtle() : base(name: "Turtle", maxHP: 6, staggers: 2, damage: 2, timer: 3, effect: InLineIcon.DAMAGE + ": 2, " + InLineIcon.ON_STAGGER + ": You get +1 Energy!", "Turtle")
    { }

    protected override void OnLossOfLife()
    {
        Player player = GameObject.Find("Player").GetComponent<Player>();
        player.AddEnergy(1);
    }
}
